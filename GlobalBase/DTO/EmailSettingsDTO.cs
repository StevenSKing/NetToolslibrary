using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalBase.DTO
{
    

    public class EmailSettingsDTO
    {
        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }

        public string HtmlForEmail { get; set; }
    }
}