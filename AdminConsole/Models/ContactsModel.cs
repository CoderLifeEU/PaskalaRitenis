﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConsole.Models
{
    public class ContactsModel
    {
        public int Id { get; set;}
        public string Vieta { get; set; }
        public string Adrese { get; set; }
        public string ExtraInfo { get; set; }
    }
}