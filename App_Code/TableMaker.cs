using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

/// <summary>
/// Made by Bruno Casali, when? At 1:40PM on wednesday and yesterday too! today is 25/06/2014
/// <para>CONTACT => Twitter: @brunoocasali, Email: brunoocasali@gmail.com, GitHub: brunoocasali.</para>
/// <para>
/// README below!
/// <!--
///    If doesn't work verify these itens on your class:
///    
///    1° - All methods used should be STATICS.
///    2° - All of you attributes should be FIELDS in other words, without: { get; set; }
///    3° - Verify if you overriden the ToString() method.
///    4° - Your number of params to the choosed method.
///    
///    ----
///         --- If you could anwser yes to all of these questions... :D
///         
///    1° - The Name of class is correct ?
///    2° - The Class / Methods / Fields are PUBLIC's ?
///    3° - This method return a List<T> ?
/// -->
/// </para>
/// </summary>

public class TableMaker
{
    public Type Class { get; set; }
    public MethodInfo Method { get; set; }
    public Object[] ParametersArray { get; set; }
    public List<FieldInfo> Attributes { get; set; }
    private String _TableCode;

    public String TableCode
    {
        get
        {
            return @"<table>
                       <thead>
                           <tr>
                           <!-- PLEASE DO NOT REMOVE THESE, There'll be inputed the table head code here -->
                           {0}
                          </tr>
                       </thead>
                       <tbody>
                          {1}
                       </tbody>
                     </table>";
        }
        set { _TableCode = value; }
    }

    /*   ----------   */

    private StringBuilder sb = new StringBuilder();
    private StringBuilder thead = new StringBuilder();
    private StringBuilder trows = new StringBuilder();
    private StringBuilder tcells = new StringBuilder();

    public TableMaker(Type classname, string methodname, object[] parameters, List<string> attrs)
    {
        try
        {
            Class = classname;//Assembly.GetCallingAssembly().GetType(classname);
            
            if (Class == null)
                throw new Exception("A classe informada - " + classname + " -  não foi encontrada / existe no seu projeto!");

            Method = Class.GetMethod(methodname);
            
            if (Method == null)
                throw new Exception("A classe informada - " + classname + " - não possuí uma declaração pública do método - " + methodname + " -!");

            ParametersArray = parameters;

            List<FieldInfo> fields = Class.GetFields().ToList(); // Obtain all fields
            Attributes = new List<FieldInfo>(fields);

            bool ok = false;
            foreach (var ofield in fields)
            {
                foreach (var it in attrs)
                {
                    if (ofield.Name == it)
                    {
                        ok = true;
                        break;
                    }
                }

                //se ok for falso, significa que ele não está na lista então remova-o
                if (!ok)
                {
                    Attributes.Remove(ofield);
                }
                ok = false;
            }
        }
        catch (Exception x)
        {
            throw new Exception(x.Message);
        }
    }

    public dynamic CallMethod()
    {
        return Method.Invoke(null, ParametersArray);
    }

    public string MakeTable()
    {
        try
        {
            dynamic lst = this.CallMethod();

            Attributes.ForEach(TableHead);
            int count = Attributes.Count;

            foreach (var item in lst)
            {
                for (int i = 0; i < count; i++)
                {
                    FieldInfo pi = item.GetType().GetField(Attributes[i].Name);
                    dynamic value = (dynamic)(pi.GetValue(item));
                    TableCell(value);
                }
                TableBody();
            }

            sb.AppendFormat(this.TableCode, thead, trows);
        }
        catch (Exception x)
        {
            throw new Exception(x.Message);
        }

        return sb.ToString();
    }


    public void TableHead(FieldInfo value)
    {
        thead.AppendFormat(@"<th>{0}</th>", value.Name.ToUpper());
    }

    public void TableBody()
    {
        trows.AppendFormat(@"<tr>{0}</tr>", tcells.ToString());
        tcells.Clear();
    }

    public void TableCell(object value)
    {
        tcells.AppendFormat(@"<td>{0}</td>", value);
    }
}