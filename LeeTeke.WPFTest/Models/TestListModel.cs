using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LeeTeke.WPFTest.Models
{
    class TestListModel
    {
        public string Title { get; set; }

        public bool Boolen { get; set; }

        public ImageSource Image { get; set; }

        public TestEnum Enum { get; set; }
    }

    enum TestEnum
    {
        你好,
        不好,
        说什么
    }
}

