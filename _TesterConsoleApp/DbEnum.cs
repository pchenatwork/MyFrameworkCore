using AppBase.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _TesterConsoleApp
{
    public class DbEnum : EnumBase<DbEnum>
    {
        #region Enumeration Elements
        /* ==========================================================================
         * Name: 
         * Description:  TypeName for reflection
         * ==========================================================================*/
        public static readonly DbEnum SQL = new DbEnum(1, "Microsoft.Data.SqlClient", @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=" + Directory.GetCurrentDirectory() + "\\Workflow.mdf;Integrated Security = True;MultipleActiveResultSets = True;");

        #endregion

        #region Constructors
        private DbEnum(int id, string name, string extra) : base(id, name, extra)
        {
        }
        #endregion
    }
}
