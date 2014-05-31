<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RealTimeDetails.aspx.cs"
    Inherits="QDHServer.ad.RealTime.RealTimeDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="../../../js/jquery-1.4.4.min.js"></script>
    <script language="javascript" src="../../../js/jquery.easyui.min.1.2.2.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../js/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../../../js/themes/icon.css">
    <script type="text/javascript">
        function waiting() {
            $.messager.alert("系统提示", "请30秒以后再试！！", "warning");
        }

        function alertWarningDeletePic() {
            $.messager.alert("系统提示", "网络异常,请稍后再试！！", "error");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <div id="pic_content" runat="server" style="">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
