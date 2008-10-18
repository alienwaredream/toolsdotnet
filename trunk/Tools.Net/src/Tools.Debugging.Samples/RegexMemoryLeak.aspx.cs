using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace Tools.Debugging.Samples
{
    public partial class RegexMemoryLeak : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int nextNumber = new Random().Next(10000000);

            Regex regex = new Regex(@"\d\w(.*)?\n" + nextNumber.ToString() + "$", RegexOptions.Compiled);

            // use regular expression

            Thread.Sleep(500);

            bool isMatch = regex.IsMatch("no chance there is a match");

            RandomNumberHolder.Text = nextNumber.ToString() + ". Execution time, ms: " + watch.ElapsedMilliseconds + ". Is match: " + isMatch;

            watch.Stop();
        }
    }
}
