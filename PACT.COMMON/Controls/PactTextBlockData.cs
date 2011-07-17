using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PACT.COMMON
{
    public class PactTextBlockData : PactControlData
    {
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Text"));
                }
            }
        }
        private string _text;

        public string Heading
        {
            get
            {
                return _Heading;
            }

            set
            {
                if (_Heading != value)
                {
                    _Heading = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Heading"));
                }
            }
        }
        private string _Heading;
    }
}
