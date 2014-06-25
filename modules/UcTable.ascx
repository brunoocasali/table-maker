<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcTable.ascx.cs" Inherits="UcTable" %>

<input type="text" id="search" placeholder="Type to search..." />

    <%=html.ToString() %>

<script src="//code.jquery.com/jquery-1.10.2.min.js"></script>

<script>
    // Write on keyup event of keyword input element
    $("#search").keyup(function () {
        _this = this;
        // Show only matching TR, hide rest of them
        $.each($("#table tbody").find("tr"), function () {
            console.log($(this).text());
            if ($(this).text().toLowerCase().indexOf($(_this).val().toLowerCase()) == -1)
                $(this).hide();
            else
                $(this).show();
        });
    });
</script>
