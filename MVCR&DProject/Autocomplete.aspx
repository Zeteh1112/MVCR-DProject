<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Autocomplete.aspx.cs" Inherits="MVCR_DProject.Autocomplete" %>

<!DOCTYPE html>  
  
<html xmlns="http://www.w3.org/1999/xhtml">  
<head runat="server">  
    <title></title>  
    <script src="Scripts/jquery-1.12.4.min.js"></script>  
    <link href="Content/bootstrap.min.css" rel="stylesheet" />  
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />  
    <script src="Scripts/bootstrap.min.js"></script>  
    <script type="text/javascript" src="http://cdn.rawgit.com/bassjobsen/Bootstrap-3-Typeahead/master/bootstrap3-typeahead.min.js"></script>  
    <script type="text/javascript">  
        $(document).ready(function () {
            $('#txtEmp').typeahead({
                hint: true,
                highlight: true,
                minLength: 1,
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Autocomplete.aspx/GetEmployeeDataSample") %>',
                        data: "{ 'SearchParam': '" + request + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            items = [];
                            map = {};
                            $.each(data.d, function (i, item) {
                                var id = item.split('-')[0];
                                var name = item.split('-')[1];
                                map[name] = { id: id, name: name };
                                items.push(name);
                            });
                            response(items);
                            $(".dropdown-menu").css("height", "auto");
                            $(".dropdown-menu").css("width", "400");
                        },
                        error: function (response) {
                            console.log(response.responseText);
                        },
                        failure: function (response) {
                            console.log(response.responseText);
                        }
                    });
                },
                updater: function (item) {
                    $('#hdnEmpId').val(map[item].id);
                    return item;
                }
            });
       });
    </script>  
</head>  
<body>  
    <form id="form1" runat="server">  
        <asp:TextBox  
            runat="server"  
            ID="txtEmp"  
            CssClass="form-control"  
            AutoCompleteType="Disabled"  
            ClientIDMode="Static"  
            Width="400" />  
        <asp:HiddenField  
            runat="server"  
            ClientIDMode="Static"  
            ID="hdnEmpId" />  
    </form>  
</body>  
</html> 