Table Maker - User Control
===========

This is an simple user control that by reflection generate to you an awesome html table with your data.

How to use:


1° First add into your web.config file:
```
<configuration>
  <system.web>
    <pages controlRenderingCompatibilityVersion="4.0">
      <controls>
        <add tagPrefix="uc" src="~/UcTable.ascx" tagName="table" />
      </controls>
    </pages>
  </system.web>
</configuration>
```
2° Create a new web form called `something.aspx`

3° Add this code to him `<uc:table ID="uc"  runat="server"/>`

It maybe seems like this:
```
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Something.aspx.cs" Inherits="Something" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Awesome html tables now!!</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc:table runat="server" ID="uc" />
        </div>
    </form>
</body>
</html>
```

4° In the code behind you need to insert the code reference at your classes and methods.
It maybe seems like this either:

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Something : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        uc.ParametersArray = new object[] { /* anything here, ALL OF YOUR METHOD PARAMS */ };
        uc.ClassName = typeof(MyClass);
        uc.MethodName = "myMethod";
        uc.Fields = new List<string>() { "my", "public", "attributes", "that", "I", "want" };
    }
}
```

And save and start your ISS server! :D
I'll be improve the code, so help me fork this repo!
