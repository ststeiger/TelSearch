

using System;
using System.Collections.Generic;
using System.Text;


namespace TelSearch
{


    class ConstraintRegex
    {


        public static List<string> GetConstraintArguments(string strSpecificConstraintName)
        {
            List<string> lsArgs = new List<string>();
            string strFunctionName = null;
            string strArguments = null;



            // TODO: Get Constraint definition 
            string strConstraint = @"
            (NOT [dbo].[fu_Constaint_MitarbeitergueltigkeitDarfKostenstellenBelegungsgueltigkeitNichtUnterOderUeberschreiten_T_AP_Kontakte]
                ([KT_UID],[KT_DatumVon],[KT_DatumBis])=(1))
";


            strConstraint = @"
            (NOT [dbo].[fu_Constaint_MitarbeitergueltigkeitDarfKostenstellenBelegungsgueltigkeitNichtUnterOderUeberschreiten_T_AP_Kontakte]
                ([KT_UID],[KT_DatumVon] - (-1) + dbo.fnlalala('abc'),[KT_DatumBis])=(1))
";


            string strSqlFunctionPattern = @"(\[?dbo\]?\.\[?(.+?)\]?\s*\(([^\(\)]*(((?<Open>\()[^\(\)]*)+((?<Close-Open>\))[^\(\)]*)+)*(?(Open)(?!)))\s*\)\s*)";
            string strSqlFunctionArgumentsPattern = @"(?:[^,()]+((?:\((?>[^()]+|\((?<open>)|\)(?<-open>))*\)))*)+";


            System.Text.RegularExpressions.Match maFunction = System.Text.RegularExpressions.Regex.Match(strConstraint, strSqlFunctionPattern, System.Text.RegularExpressions.RegexOptions.Singleline);
            for (int i = 0; i < maFunction.Length; ++i)
            {
                //string strExpression = maFunction.Groups[1].Value;
                strFunctionName = maFunction.Groups[2].Value;
                strArguments = maFunction.Groups[3].Value;

                // Console.WriteLine(maFunction.Groups[i].Value);
            }


            var maArguments = System.Text.RegularExpressions.Regex.Matches(strArguments, strSqlFunctionArgumentsPattern);
            for (int i = 0; i < maArguments.Count; ++i)
            {
                lsArgs.Add(maArguments[i].Value);
                // Console.WriteLine(maArguments[i].Value);
            }

            return lsArgs;
        } // End Function GetConstraintArguments


    } // End Class ConstraintRegex 


} // End Namespace TelSearch 


// http://www.search.ch/jobs/engineer.html
