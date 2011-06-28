using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PACT.Service
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Collections;
    using System.Threading;
    using System.Configuration;
    using System.Security.Cryptography;
    using System.Text;
    using System.Runtime.Serialization;

    /// <summary>
    ///
    ///		Provides an abstraction of database functions
    ///		
    /// </summary>
    public class SQLAdapter
    {
        private static ReaderWriterLock m_ProcedureCacheLock = new ReaderWriterLock();
        private static Hashtable m_ProcedureCache = new Hashtable();

        private static System.Collections.Generic.Dictionary<int, string> ConnectionCache =
            new System.Collections.Generic.Dictionary<int, string>();

        static SQLAdapter()
        {
        }

        /// <summary>
        /// 
        ///		Exceptions
        ///		
        /// </summary>
        public class DbUtilException : System.Exception
        {

            private int m_nErrorCode = -1;

            public DbUtilException()
            {
            }

            public DbUtilException(string message)
                : base(message)
            {
            }

            public DbUtilException(string message, int errCode)
                : base(message)
            {

                m_nErrorCode = errCode;
            }

            public DbUtilException(string message, Exception inner)
                : base(message, inner)
            {
            }

            public DbUtilException(string message, Exception inner, int errCode)
                : base(message, inner)
            {

                m_nErrorCode = errCode;
            }

            public int ErrorCode
            {

                get
                {

                    return m_nErrorCode;
                }

                set
                {

                    m_nErrorCode = value;
                }
            }
        }

        /// <summary>
        /// 
        ///		An internal abstraction of a procedure.
        ///		
        /// </summary>

        private class DbUtilProcedure
        {

            public string ProcedureName;
            public ArrayList ProcedureParameters = new ArrayList();

            public SqlCommand Command(
                SqlConnection connection,
                ArrayList values)
            {

                SqlCommand command = new SqlCommand(
                    ProcedureName,
                    connection
                );

                if (values != null)
                {

                    int i = 0;
                    foreach (DbUtilParameter parameter in ProcedureParameters)
                    {

                        SqlParameter cparam = command.Parameters.Add(
                            parameter.ParameterName,
                            parameter.ParameterType,
                            parameter.ParameterLength
                            );
                        cparam.Direction = parameter.ParameterDirection;
                        if (parameter.ParameterDirection == ParameterDirection.Input)
                        {
                            if (values.Count > i)
                            {
                                cparam.Value = values[i++];
                            }
                        }
                    }
                }
                command.CommandType = CommandType.StoredProcedure;
                return command;
            }
        }

        /// <summary>
        /// 
        ///		An internal abstraction of parameter information
        ///		
        /// </summary>

        private class DbUtilParameter
        {

            public string ParameterName;
            public SqlDbType ParameterType;
            public int ParameterLength;
            public ParameterDirection ParameterDirection;

            private class TypeMap
            {
                public string Name;
                public SqlDbType Type;
                public TypeMap(string name, SqlDbType type)
                {
                    Name = name; Type = type;
                }
            }

            private static TypeMap[] Types = new TypeMap[] {
				new TypeMap( "bit",		SqlDbType.Bit),
				new TypeMap( "char",	SqlDbType.Char ),
				new TypeMap( "bigint",	SqlDbType.BigInt ),
				new TypeMap( "binary",	SqlDbType.Binary ),
				new TypeMap( "datetime",SqlDbType.DateTime ),
				new TypeMap( "decimal",	SqlDbType.Decimal ),
				new TypeMap( "float",	SqlDbType.Float ),
				new TypeMap( "image",	SqlDbType.Image ),
				new TypeMap( "int",		SqlDbType.Int ),
				new TypeMap( "money",	SqlDbType.Money ),
				new TypeMap( "nchar",	SqlDbType.NChar ),
				new TypeMap( "numeric",	SqlDbType.Int ),
				new TypeMap( "nvarchar",SqlDbType.NVarChar ),
				new TypeMap( "real",	SqlDbType.Real ),
				new TypeMap( "smalldatetime",	SqlDbType.SmallDateTime ),
				new TypeMap( "smallint",	SqlDbType.SmallInt ),
				new TypeMap( "smallmoney",	SqlDbType.SmallMoney ),
				new TypeMap( "timestamp",	SqlDbType.Timestamp ),
				new TypeMap( "tinyint",	SqlDbType.TinyInt ),
				new TypeMap( "uniqueidentifier",SqlDbType.UniqueIdentifier ),
				new TypeMap( "varbinary",	SqlDbType.VarBinary ),
				new TypeMap( "varchar",	SqlDbType.VarChar ),
				new TypeMap( "variant",	SqlDbType.Variant ),
				new TypeMap( "ntext",	SqlDbType.Text )
			};

            public static SqlDbType StringToType(string typeName)
            {

                SqlDbType result = SqlDbType.Variant;
                foreach (TypeMap typeMap in Types)
                {

                    if (typeMap.Name.ToLower() == typeName)
                    {

                        result = typeMap.Type;
                        break;
                    }
                }
                return result;
            }

            public static ParameterDirection IntToDirection(int direction)
            {

                ParameterDirection result = ParameterDirection.InputOutput;
                switch (direction)
                {
                    case 0:
                        result = ParameterDirection.Input;
                        break;
                    case 1:
                        result = ParameterDirection.Output;
                        break;
                }
                return result;
            }
        }

        /// <summary>
        /// 
        ///		An external wrapper of a SP command result
        ///		
        /// </summary>

        public class DbUtilReaderResult
        {

            public ArrayList HashList = new ArrayList();
            public Hashtable Parameters = new Hashtable();
            public object Return = null;
        }

        /// <summary>
        /// 
        ///		Return the cached procedure.
        ///		
        /// </summary>
        /// <param name="connection">
        /// 
        ///		The connection to use to get the procedure detail
        ///
        /// </param>
        /// <param name="procedureName">
        /// 
        ///		The procedure identifier.
        ///		
        /// </param>
        /// <returns>
        /// 
        ///		A procedure.
        ///		
        /// </returns>

        private static DbUtilProcedure Procedure(
            SqlConnection connection,
            string procedureName, SqlTransaction oTransaction)
        {

            DbUtilProcedure procedure = null;
            if (!m_ProcedureCache.Contains(procedureName))
            {

                SqlDataReader reader = null;

                try
                {

                    m_ProcedureCacheLock.AcquireWriterLock(Timeout.Infinite);

                    if (!m_ProcedureCache.Contains(procedureName))
                    {

                        procedure = new DbUtilProcedure();

                        SqlCommand command = new SqlCommand("spUTL_GetProcedureMetaData", connection);
                        command.Parameters.AddWithValue("@objname", procedureName);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Transaction = oTransaction;

                        reader = command.ExecuteReader();

                        procedure.ProcedureName = procedureName;

                        while (reader.Read())
                        {

                            DbUtilParameter parameter = new DbUtilParameter();

                            parameter.ParameterName = reader.GetString(0);
                            parameter.ParameterLength = reader.GetInt32(2);
                            parameter.ParameterType =
                                DbUtilParameter.StringToType(reader.GetString(1));
                            parameter.ParameterDirection =
                                DbUtilParameter.IntToDirection(reader.GetInt32(5));

                            procedure.ProcedureParameters.Add(parameter);
                        }
                        {
                            //Adding a Return Type as the SP can't return that value.
                            DbUtilParameter parameter = new DbUtilParameter();

                            parameter.ParameterName = "@RETURN_VALUE";
                            parameter.ParameterLength = 4;
                            parameter.ParameterType = SqlDbType.Int;
                            parameter.ParameterDirection = ParameterDirection.ReturnValue;

                            procedure.ProcedureParameters.Add(parameter);
                        }

                        // Update Cache
                        m_ProcedureCache[procedureName] = procedure;
                    }
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
                finally
                {

                    if (reader != null)
                    {

                        reader.Close();
                    }
                    m_ProcedureCacheLock.ReleaseWriterLock();
                }
            }

            try
            {

                m_ProcedureCacheLock.AcquireReaderLock(Timeout.Infinite);
                procedure = (DbUtilProcedure)(m_ProcedureCache[procedureName]);
            }
            catch (System.Exception)
            {

                // do nothing
            }
            finally
            {

                m_ProcedureCacheLock.ReleaseReaderLock();
            }
            return procedure;
        }


        public static SQLDBResult Execute(
            string procedureName,
            ArrayList procedureParameters, string strConnection)
        {

            SQLDBResult result = null;
            SqlConnection connection = null;
            try
            {

                // Get Connection and Procedure
                connection = new SqlConnection(strConnection);
                connection.Open();
                DbUtilProcedure procedure = Procedure(connection, procedureName, null);

                // Create Command 
                SqlCommand command = procedure.Command(connection, procedureParameters);
                command.CommandTimeout = 0;
                result = new SQLDBResult();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                // Execute and Fill Results
                adapter.Fill(result.Contents);
                foreach (SqlParameter cparam in command.Parameters)
                {

                    switch (cparam.Direction)
                    {

                        case ParameterDirection.InputOutput:
                        case ParameterDirection.Output:
                        case ParameterDirection.ReturnValue:

                            result.Parameters[cparam.ParameterName] = cparam.Value;
                            break;

                        case ParameterDirection.Input:
                        default:

                            // do nothing
                            break;
                    }
                }
            }
            catch (System.Exception e)
            {

                
                throw new DbUtilException(e.Message, e);
            }
            finally
            {

                if ((connection != null) &&
                    (connection.State == ConnectionState.Open))
                {

                    connection.Close();
                }
            }
            return result;
        }

        public static string GetConnection(int CompanyIndex)
        {
            if (!ConnectionCache.ContainsKey(CompanyIndex))
            {

                if (System.Configuration.ConfigurationManager.AppSettings["DBAuthentication"] == "SQLServer")
                {
                    ConnectionCache.Add(CompanyIndex, "Data Source=" + System.Configuration.ConfigurationManager.AppSettings["DBServer"]
                          + ";Initial Catalog=PACT2C" + CompanyIndex.ToString()
                          + ";Persist Security Info=True;User ID=" + System.Configuration.ConfigurationManager.AppSettings["DBUserID"]
                          + ";Password=" + System.Configuration.ConfigurationManager.AppSettings["DBPassword"]);// PACTCryptoEngine.Decrypt(System.Configuration.ConfigurationSettings.AppSettings["DBPassword"], true));
                }
                else
                {
                    ConnectionCache.Add(CompanyIndex, "Data Source=" + System.Configuration.ConfigurationManager.AppSettings["DBServer"]
                         + ";Initial Catalog=PACT2C" + CompanyIndex + ";Trusted_Connection=Yes;");
                }

            }

            return ConnectionCache[CompanyIndex];
        }
    }


    public class SQLDBResult
    {

        public DataSet Contents = new DataSet();


        public Hashtable Parameters = new Hashtable();


        public object Return = null;
    }

    public class PACTCryptoEngine
    {
        static string _SecurityKey = string.Empty;
        public static string SecurityKey
        {
            get { return _SecurityKey; }
            set { _SecurityKey = value; }
        }

        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            // Get the key from config file
            if (_SecurityKey == null)
                _SecurityKey = string.Empty;

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_SecurityKey));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(_SecurityKey);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //Get your key from config file to open the lock!
            // string key = System.Configuration.ConfigurationManager.AppSettings["SecurityKey"];
            if (_SecurityKey == null)
                _SecurityKey = string.Empty;

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_SecurityKey));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(_SecurityKey);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
