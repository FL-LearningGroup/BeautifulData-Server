using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Framework.Models
{
    public static class MailBodyFormat
    {
        public static int MaxMailMessageSize { get { return 1024 * 25; } }
        public static string Header { get { return "header"; } }
        public static string Message { get { return "message"; } }
        public static string HTMLTable
        {
            get
            {
                string text = @"<!DOCTYPE html>
                        <html>
                        <head>
                        <style>
                        table {
                            font-family: arial, sans-serif;
                            border-collapse: collapse;
                        }

                        td, th {
                            border: 1px solid #dddddd;
                            text-align: left;
                            padding: 8px;
                        }

                        tr:nth-child(even) {
                            background-color: #dddddd;
                        }
                        </style>
                        </head>
                        <body>
                        <h2>Hi All,<h2>
                        <table>
                            <tr>" +
                             Header + @"
                            </tr>" +                            
                             Message + @"
                        </table>
                        <h2>Thanks</h2>
                        </body>
                        </html>
                        ";
                return text;
            }
        }
    }
}
