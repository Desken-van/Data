using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Models
{
    public class SMSModel
    {
        public int Sender { get; set; }
        public string Sms { get; set; }
        public int Recipient { get; set; }
        public int Number { get; set; }
    }
}
