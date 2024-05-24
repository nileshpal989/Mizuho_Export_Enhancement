<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Impw_ViewSupplierMaster.aspx.cs"
    Inherits="ImportWareHousing_Masters_Supplier_Master" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>LMCC-TRADE FINANCE System</title>
    <link href="../../Style/style_new.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="jquery-1.8.3.min.js"></script>
    <script src="ASPSnippets_Pager.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("#ddlrecordperpage").change(function () {
                    //alert($('option:selected', this).text());
                    var ddl = $("#ddlrecordperpage option:selected").text();
                    GetCustomers(1, ddl);
                });
            });

            $(function () {
                var ddl = $("#ddlrecordperpage option:selected").text();
                GetCustomers(1, ddl);
            });
            $("[id*=txtSearch]").live("keyup", function () {
                var ddl = $("#ddlrecordperpage option:selected").text();
                GetCustomers(parseInt(1), ddl);
            });

            $("[id*=txtSearch]").on('input', function () {
                var ddl = $("#ddlrecordperpage option:selected").text();
                GetCustomers(parseInt(1), ddl);
            });

            $(".Pager .page").live("click", function () {
                var ddl = $("#ddlrecordperpage option:selected").text();
                GetCustomers(parseInt($(this).attr('page')), ddl);
            });
            function SearchTerm() {
                return jQuery.trim($("[id*=txtSearch]").val());
            };
            function GetCustomers(pageIndex, pageSize) {
                $.ajax({
                    type: "POST",
                    url: "Impw_ViewSupplierMaster.aspx/GetCustomers",
                    data: '{searchTerm: "' + SearchTerm() + '", pageIndex: ' + pageIndex + ', pageSize: ' + pageSize + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        alert(response.d);
                    },
                    error: function (response) {
                        alert(response.d);
                    }
                });
            }
            var row;
            function OnSuccess(response) {
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var customers = xml.find("Customers");
                if (row == null) {
                    row = $("[id*=GridViewCustomerList] tr:last-child").clone(true);
                }
                $("[id*=GridViewCustomerList] tr").not($("[id*=GridViewCustomerList] tr:first-child")).remove();
                if (customers.length > 0) {
                    $.each(customers, function () {
                        var customer = $(this);

                        if ($(this).find("CUST_NAME").text().length > 30) {
                            $("td", row).eq(0).html($(this).find("CUST_NAME").text().substring(0, 30) + "...");
                        }
                        else {
                            $("td", row).eq(0).html($(this).find("CUST_NAME").text());
                        }

                        $("td", row).eq(0).attr('title', $(this).find("CUST_NAME").text());
                        $("td", row).eq(1).html($(this).find("Cust_ACNo").text());
                        $("td", row).eq(2).html($(this).find("Supplier_ID").text());

                        if ($(this).find("Supplier_Name").text().length > 30) {
                            $("td", row).eq(3).html($(this).find("Supplier_Name").text().substring(0, 30) + "...");
                        }
                        else {
                            $("td", row).eq(3).html($(this).find("Supplier_Name").text());
                        }

                        $("td", row).eq(3).attr('title', $(this).find("Supplier_Name").text());

                        if ($(this).find("Supplier_Address").text().length > 53) {
                            $("td", row).eq(4).html($(this).find("Supplier_Address").text().substring(0, 50) + "...");
                        }
                        else {
                            $("td", row).eq(4).html($(this).find("Supplier_Address").text());
                        }

                        $("td", row).eq(4).attr('title', $(this).find("Supplier_Address").text());
                        $("td", row).eq(5).html($(this).find("SupplierCountryName").text());
                        $("td", row).eq(6).html($(this).find("Bank_Name").text());
                        $("td", row).eq(7).html($(this).text("").append($('<input type="button"  class="deleteButton" value="Delete"/>')));

                        $("[id*=GridViewCustomerList]").append(row);
                        row = $("[id*=GridViewCustomerList] tr:last-child").clone(true);
                    });
                    $('#GridViewCustomerList tr th:nth-child(2').hide();
                    $('#GridViewCustomerList tr td:nth-child(2').hide();
                    var pager = xml.find("Pager");
                    $(".Pager").ASPSnippets_Pager({
                        ActiveCssClass: "current",
                        PagerCssClass: "pager",
                        PageIndex: parseInt(pager.find("PageIndex").text()),
                        PageSize: parseInt(pager.find("PageSize").text()),
                        RecordCount: parseInt(pager.find("RecordCount").text())
                    });

                    $(".CUST_NAME").each(function () {
                        var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                        //$(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                        $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm().toUpperCase() + "</span>"));
                    });
                    $(".Supplier_ID").each(function () {
                        var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                        $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm().toUpperCase() + "</span>"));
                    });
                    $(".Supplier_Name").each(function () {
                        var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                        $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm().toUpperCase() + "</span>"));
                    });
                } else {
                    var empty_row = row.clone(true);
                    $("td:first-child", empty_row).attr("colspan", $("td", row).length);
                    $("td:first-child", empty_row).attr("align", "center");
                    $("td:first-child", empty_row).html("No records found for the search criteria.");
                    $("td", empty_row).not($("td:first-child", empty_row)).remove();
                    $("[id*=GridViewCustomerList]").append(empty_row);
                }
            };


            $('#GridViewCustomerList tr th:nth-child(2').hide();
            $('#GridViewCustomerList tr td:nth-child(2').hide();

            $('#GridViewCustomerList tr td').click(function () {
                var currentRow = $(this).closest("tr");

                //var col1 = currentRow.find("td:eq(0)").text(); // get current row 1st TD value
                var CustAcNo = currentRow.find("td:eq(1)").text(); // get current row 2nd TD
                var SupplierID = currentRow.find("td:eq(2)").text(); // get current row 3rd TD
                var ADCode = $('#ddlBranch').val();

                var td = this.cellIndex;
                if (td < 7) {
                    //alert(td);
                    pageurl = "Impw_AddEditSupplierMaster.aspx?Mode=Edit&CustACNo=" + CustAcNo + "&SupplierID=" + SupplierID + "&Branch=" + ADCode + "'";
                    window.location.href = pageurl;


                }
            })

            // code to read selected table row cell data (values).
            $("#GridViewCustomerList").on('click', '.deleteButton', function () {
                // get the current row

                var a = confirm('Are you sure,Dou you want to delete this record?')
                if (a) {
                    var currentRow = $(this).closest("tr");

                    //var col1 = currentRow.find("td:eq(0)").text(); // get current row 1st TD value
                    var CustAcNo = currentRow.find("td:eq(1)").text(); // get current row 2nd TD
                    var SupplierID = currentRow.find("td:eq(2)").text(); // get current row 3rd TD
                    //var data = col1 + "\n" + col2 + "\n" + col3;

                    var ADCode = $('#ddlBranch').val();


                    $.ajax({
                        type: "POST",
                        url: "Impw_ViewSupplierMaster.aspx/DeleteCustomer",
                        //data: '{CustomerACNo: "' + CustAcNo + '"}',

                        data: '{CustomerACNo: "' + CustAcNo + '", SupplierID: "' + SupplierID + '", ADCode: "' + ADCode + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: OnDeleteSuccess,
                        failure: function (response) {
                            alert(response.d);
                        },
                        error: function (response) {
                            alert(response.d);
                        }
                    });
                }
            });

            function OnDeleteSuccess(response) {
                if (response.d.toString() == "Deleted") {
                    alert("Record Deleted");
                } else {
                    alert("Error While Deleting Record");
                }
                var ddl = $("#ddlrecordperpage option:selected").text();
                GetCustomers(1, ddl);
            }
        });


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/Enable_Disable_Opener.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom" colspan="2">
                            <span class="pageLabel"><strong>Supplier Master View</strong> </span>
                        </td>
                        <td align="right" style="width: 50%">
                            <input type="hidden" id="hdnUserRole" runat="server" />
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="buttonDefault"
                                ToolTip="Upload Supplier Master File" OnClick="btnUpload_Click" TabIndex="1" />
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Supplier"
                                OnClick="btnAdd_Click" TabIndex="2" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 50%" align="left">
                            <span class="elementLabel">Branch :</span>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td align="right" style="width: 100%;" valign="top">
                            <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                CssClass="textBox" MaxLength="40" Width="180px" TabIndex="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td align="left" style="vertical-align: top" colspan="3">
                            <asp:GridView ID="GridViewCustomerList" runat="server" AutoGenerateColumns="False"
                                Width="90%" CssClass="GridView">
                                <Columns>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustName" runat="server" ToolTip='<%# Eval("CUST_NAME") %>' Text='<%# Eval("CUST_NAME") %>'
                                                CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" CssClass="CUST_NAME" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer A/C NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustName" runat="server" CssClass="Cust_ACNo" ToolTip='<%# Eval("Cust_ACNo") %>'
                                                Text='<%# Eval("Cust_ACNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierID" runat="server" ToolTip='<%# Eval("Supplier_ID") %>'
                                                Text='<%# Eval("Supplier_ID") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="Supplier_ID" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierName" runat="server" ToolTip='<%# Eval("Supplier_Name") %>'
                                                Text='<%# Eval("Supplier_Name") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" CssClass="Supplier_Name" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierAddress" runat="server" CssClass="Supplier_Address" ToolTip='<%# Eval("Supplier_Address") %>'
                                                Text='<%# (Eval("Supplier_Address").ToString().Length > 50) ? (Eval("Supplier_Address").ToString().Substring(0, 50) + "...") : Eval("Supplier_Address")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierCountry" runat="server" CssClass="SupplierCountryName"
                                                ToolTip='<%# Eval("SupplierCountryName") %>' Text='<%# Eval("SupplierCountryName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankName" runat="server" CssClass="Bank_Name" ToolTip='<%# Eval("Bank_Name") %>'
                                                Text='<%# Eval("Bank_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("Supplier_ID") %>'
                                                CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Supplier ID" CssClass="deleteButton" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="rowPager" runat="server">
                        <td align="left" style="vertical-align: top" colspan="3" class="gridHeader">
                            <table cellspacing="0" cellpadding="3" width="80%" border="0">
                                <tbody>
                                    <tr>
                                        <td align="left" style="width: 25%">
                                            &nbsp;Records per page:&nbsp;
                                            <select id="ddlrecordperpage" name="ddlrecordperpage" class="dropdownList">
                                                <option value="10">10</option>
                                                <option value="20" selected="selected">20</option>
                                                <option value="30">30</option>
                                                <option value="40">40</option>
                                                <option value="50">50</option>
                                            </select>
                                        </td>
                                        <td align="center" style="width: 75%" valign="top">
                                            <div class="Pager">
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
