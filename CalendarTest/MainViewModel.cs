﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace CalendarTest
{
    [ImplementPropertyChanged]
    public class MainViewModel
    {
        public DateTime Date { get; set; }
    }
}
