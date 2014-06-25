using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Text;

public partial class UcTable : UserControl
{
    public Type ClassName { get; set; }
    public string MethodName { get; set; }
    public object[] ParametersArray { get; set; }
    public List<string> Fields { get; set; }
    public String TableCode { get; set; }

    protected StringBuilder html = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        TableMaker table = new TableMaker(ClassName, MethodName, ParametersArray, Fields);
        table.TableCode = !string.IsNullOrEmpty(this.TableCode) ? table.TableCode : this.TableCode;

        html.AppendFormat(table.MakeTable());
    }
}