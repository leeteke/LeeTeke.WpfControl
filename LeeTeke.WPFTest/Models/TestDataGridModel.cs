using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LeeTeke.WPFTest.Models
{
    internal class TestDataGridModel
    {
        public int No { get; set; }
        public string Id { get; set; }

        public string Name { get; set; }

        public string Intro { get; set; }

        public string Group { get; set; }

        public string StatusA { get; set; }

        public string StatusB { get; set; }

        public string StatusC { get; set; }

        public string StatusD { get; set; }

        public string StatusE { get; set; }

        public string StatusF { get; set; }


        public DateTime CreateTIme { get; set; }

        public TestAEnum TEnum { get; set; }
    }

    public enum TestAEnum
    {
        A,
        B,
        C,
        D,
        E
    }
}
