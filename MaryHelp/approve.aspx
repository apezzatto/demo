<%@ Page Language="C#" AutoEventWireup="true" CodeFile="approve.aspx.cs" Inherits="approve" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approve your candidate</title>
    <link rel="stylesheet" type="text/css" href="css/styles.css" title="Style1">
</head>
<body>
    <form id="form1" runat="server">
        <div>
		    <div class="top">
                <div class="logo">Clean me now!</div>
                <div class="about" runat="server"><a href="">About Us</a></div>
		    </div>            
		    <div id="workerdetails" runat="server">
                <table id="approval">
                <tr>
                    <td style="width:50%">
                        <table style="width:100%; border-style:solid; border-width:1px;">
		                    <tr class="header">
			                    <th>
				                    Job Details
			                    </th>
		                    </tr>
                            <tr>
                                <td>
                                    <table style="width:100%">
				                        <tr>
					                        <td class="label"><b>When: </b></td>
					                        <td>
                                                <asp:Label ID="lblWhen" runat="server"></asp:Label>
                                            </td>
				                        </tr>
				                        <tr>
					                        <td class="label"><b>Time: </b></td>
					                        <td>
                                                <asp:Label ID="lblTime" runat="server"></asp:Label>
                                            </td>
				                        </tr>
                                        <tr>
					                        <td class="label"><b>Duration:</b></td>
					                        <td>
                                                <asp:Label ID="lblHours" runat="server"></asp:Label>
                                            </td>
				                        </tr>
		                                <tr>
					                        <td class="label"><b>Estimated End Time </b></td>
					                        <td>
                                                <asp:Label ID="lblFinishTime" runat="server"></asp:Label>
                                            </td>
		                                </tr>
				                        <tr>
					                        <td class="label"><b>Price: </b></td>
					                        <td>
                                                <asp:Label ID="lblPrice" runat="server"></asp:Label>
                                            </td>
				                        </tr>
			                        </table>
                                </td>                     
                            </tr>
		                    <tr>
			                    <td class="bottom">

			                    </td>
		                    </tr>
                        </table>
                    </td>
                    <td style="width:50%">
                        <table style="width:100%; border-style:solid; border-width:1px;">
		                    <tr>
			                    <th class="header">
				                    Candidate Details
			                    </th>
		                    </tr>
                            <tr>
                                <td>
                                    <table style="width:100%">
				                        <tr>
					                        <td class="label"><b>Name: </b></td>
					                        <td>
                                                <asp:Label ID="lblName" runat="server"></asp:Label>
                                            </td>
				                        </tr>
				                        <tr>
					                        <td class="label"><b>Address: </b></td>
					                        <td>
                                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                            </td>
				                        </tr>
				                        <tr>
					                        <td class="label"><b>City: </b></td>
					                        <td>
                                                <asp:Label ID="lblCity" runat="server"></asp:Label>
                                            </td>
				                        </tr>
                                        <tr>
					                        <td class="label"><b>Neighborhood: </b></td>
					                        <td>
                                                <asp:Label ID="lblNeighborhood" runat="server"></asp:Label>
                                            </td>
				                        </tr>
		                                <tr>
					                        <td class="label"><b>E-Mail: </b></td>
					                        <td>
                                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                            </td>
		                                </tr>
			                        </table>
                                </td>                     
                            </tr>
		                    <tr>
			                    <td class="bottom">

			                    </td>
		                    </tr>
                        </table>
                    </td>

                    </tr>
		            <tr>
			            <td colspan="2" class="bottom">
                            <asp:Button ID="btnDeny" runat="server" Text="Reprove" CssClass="fancybuttoncancel" OnClick="btnDeny_Click" />
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="fancybutton" OnClick="btnApprove_Click" />
			            </td>
		            </tr>
                </table>
            </div>
            <p id="message">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label><br />
                <asp:Label ID="lblMessage" runat="server" ForeColor="#006600" Font-Bold="True"></asp:Label><br />
            </p>
        </div>
        <footer>&copy; All rights reserved</footer>
    </form>
</body>
</html>
