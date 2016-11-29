<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportGestanteProcedenciaASPX.aspx.cs" Inherits="Telemonitoreo.Views.Report.ReportGestanteProcedenciaASPX" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    &nbsp;<title>ReportViwer in MVC4 Application</title>    
</head>
<body>
    <form id="formGesProcedencia" runat="server">
        <input name="FechaIHidden" id="FechaIHidden" type="hidden" />
        <input name="FechaFHidden" id="FechaFHidden" type="hidden" />
        <input name="RegionHidden" id="RegionHidden" type="hidden" />
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false" SizeToReportContent="true" Width="633px">
        </rsweb:ReportViewer>
        
    </div>
    </form>
</body>
</html>
