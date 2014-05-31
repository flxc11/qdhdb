$(document).ready(function() {
    $('#LoginBtn').bind('click', function () { LoginSubmit(); });
    $('#TxtAdminName').focus();
    $('#TxtAdminName').keypress(function (e) { var curKey = e.which; if (curKey == 13) { LoginSubmit(); } });
    $('#TxtAdminPass').keypress(function (e) { var curKey = e.which; if (curKey == 13) { LoginSubmit(); } });
});

function LoginSubmit()
{
	if ($("#TxtAdminName").val()=="" || $("#TxtAdminPass").val()=="")
	{
		alert("登录帐号跟登录密码均不能为空。");
	}
	else
	{
		var params = {
			AdminName: $("#TxtAdminName").val(),
			AdminPass: $("#TxtAdminPass").val()
		};
		$.ajax({
		    type: "POST",
		    dataType: "json",
		    url: "Login.aspx?Action=Login&Time=" + (new Date().getTime()),
		    data: params,
		    success: function (d) {
		        if (d.msgCode == '1') {
		            window.location.href = d.goUrl;
		        }
		        else {
		            alert(d.msgStr);
		        }
		    }
		});
	}
}