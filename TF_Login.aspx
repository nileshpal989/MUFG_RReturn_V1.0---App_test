<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_Login.aspx.cs" Inherits="TF_Login" %>

<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link type="text/css" href="Style/style_new.css" rel="stylesheet" media="screen"/>    
    <link href="Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <link type="text/css" href="Style/2.d1e050c9.chunk.css" rel="stylesheet" media="screen" />
    <link type="text/css" href="Style/main.25dd05f5.chunk.css" rel="stylesheet" media="screen" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="Scripts/crypto-js.min.js" type="text/javascript" language="javascript"></script>
    <title>LMCC Trade Finance System</title>
    <script language="javascript" type="text/javascript">
        
        function FirstLogin() {
            var msg = 'Please Change Password after First Login.';
            alert(msg);
            var btrue = false;

            //            var Ok = confirm('Do you want to change it now?');


            //            if (Ok) {
            Call();
            btrue = true;
            //            }
            //            else {
            //                //                Call2();
            //                //                btrue = false;
            //            }


            return btrue;
        }

        function OpenUserList() {

            open_popup('TF_UserLookUp.aspx?PageHeader=User LookUp', 400, 200, 'UserList');
            return false;
        }
        function selectUser(Uname) {

            document.getElementById('txtUserName').value = Uname;
            document.getElementById('txtUserName').focus();
        }



        function validateSave() {
            var _userName = document.getElementById('txtUserName');
            var _password = document.getElementById('txtPassword');
            var _retypePassword = document.getElementById('txtReTypePassword');
            var _role = document.getElementById('ddlRole');

            if (_userName.value == '') {
                alert('Enter User Name.');
                _userName.focus();
                return false;
            }

            if (_password.value == '') {
                alert('Enter Password.');
                _password.focus();
                return false;
            }

            return true;
        }


        function ConfirmCahngePaswd() {            

            var Ok = confirm('Your Password will expire in ' + document.getElementById('hdmremaindays').value + ' days. Do you want to change it now?');
            var btrue;

            if (Ok) {
                Call();
                btrue = true;
            }
            else {
                Call2();
                btrue = false;
            }


            return btrue;
        }

        function Call() {
            document.getElementById('Hidden1').value = '';
            document.getElementById('Hidden1').value = '1';
            document.getElementById('btnalert').click();
        }

        function Call2() {
            document.getElementById('Hidden1').value = '';
            document.getElementById('Hidden1').value = '0';
            document.getElementById('btnalert').click();
        }

        function ConfirmCahngePaswdExpired() {           

            var Ok = confirm('Your Password has expired. Do you want to change it now?');
            var btrue;

            if (Ok) {
                Call();
                btrue = true;
            }
            else {
                //                Call2();
                //                btrue = false;
            }


            return btrue;
        }
    </script>
    <style type="text/css">
        #app, #root
        {
            position: absolute;
            top: 0;
            left: 0;
            width: 100vw;
            height: 100%;
            overflow: hidden;
        }
        #root
        {
            z-index: 0;
        }
        #app
        {
            z-index: 1;
        }
        #app_loader
        {
            pointer-events: none;
            opacity: 0;
            transition: .2s;
        }
        #app_loader.load
        {
            opacity: 1;
            transition: .8s;
        }
    </style>
    <style type="text/css">
        .cls
        {
            margin-right: -33px;
        }
        .help_bt1 {
	background-image:url(Style/images/help_new.png);
	width: 16px;
    height: 16px;
    border: none;
    margin-bottom: 19px;
    float: left;
}
    </style>
</head>
<body class="body2" style="margin: 0px; padding: 0px; background-position: 0px 62.5%;
    transition: background-position 60s ease 0s;" class="fade_in">
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnLogin">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="Scripts/InitEndRequest.js"></script>
    <script src="Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <div id="root">
        <div class="loginPage ">
        <input type="hidden" id="hdnAdPath" runat="server" value="LDAP://uat.lmccsoft.com"/>
        <input type="hidden" id="hdndomain" runat="server" value="uat.lmccsoft.com"/>
            <div class="animated_background" style="background-color: #12294D">
                <asp:Label ID="labelMessage" runat="server" Style="color: Red; font-size: xx-large;"></asp:Label>
                <%--<asp:Button ID="btnalert" runat="server" OnClick="btnalert_Click" />--%>
                <input type="hidden" id="hdmremaindays" runat="server" />
                <input type="hidden" id="Hidden1" runat="server" />
                <input type="hidden" id="hidden2" runat="server" />
                <input type="hidden" id="inputSP" runat="server" style="visibility:hidden;"/>
                <asp:Label ID="lbldomainAll" runat="server" Text="uat.lmccsoft.com" style="visibility:hidden;"></asp:Label>
                <asp:Label ID="lbldomain" runat="server" Text="server2.uat.lmccsoft.com" style="visibility:hidden;"></asp:Label>
                <asp:Label ID="lbladPath" runat="server" Text="LDAP://server2.uat.lmccsoft.com" style="visibility:hidden;"></asp:Label>
                <div class="animated_shape circle speed-c" style="width: 0.476657px; height: 0.476657px;
                    animation-delay: -3.51564s; animation-duration: 36.7943s; left: 5.40631vw;">
                </div>
                <div class="animated_shape speed-a" style="width: 1.78063px; height: 1.78063px; animation-delay: -3.76699s;
                    animation-duration: 32.7108s; left: 63.7212vw;">
                </div>
                <div class="animated_shape speed-b" style="width: 4.8858px; height: 4.8858px; animation-delay: -19.9226s;
                    animation-duration: 41.8172s; left: 4.44219vw;">
                </div>
                <div class="animated_shape speed-a" style="width: 7.65769px; height: 7.65769px; animation-delay: -20.8499s;
                    animation-duration: 33.5832s; left: 13.7643vw;">
                </div>
                <div class="animated_shape speed-c" style="width: 7.30847px; height: 7.30847px; animation-delay: -10.2287s;
                    animation-duration: 39.8106s; left: 77.5447vw;">
                </div>
                <div class="animated_shape speed-a" style="width: 4.98055px; height: 4.98055px; animation-delay: -23.6857s;
                    animation-duration: 33.5829s; left: 2.81453vw;">
                </div>
                <div class="animated_shape speed-b" style="width: 0.379587px; height: 0.379587px;
                    animation-delay: -5.18071s; animation-duration: 43.3295s; left: 39.6603vw;">
                </div>
                <div class="animated_shape speed-b" style="width: 1.4585px; height: 1.4585px; animation-delay: -19.1567s;
                    animation-duration: 41.2102s; left: 87.0506vw;">
                </div>
                <div class="animated_shape speed-b" style="width: 1.65757px; height: 1.65757px; animation-delay: -32.6341s;
                    animation-duration: 41.1571s; left: 47.7615vw;">
                </div>
                <div class="animated_shape speed-c" style="width: 0.220895px; height: 0.220895px;
                    animation-delay: -30.3739s; animation-duration: 39.4921s; left: 67.3813vw;">
                </div>
                <div class="animated_shape speed-a" style="width: 9.47378px; height: 9.47378px; animation-delay: -25.2156s;
                    animation-duration: 32.3889s; left: 7.6386vw;">
                </div>
                <div class="animated_shape circle speed-b" style="width: 6.32122px; height: 6.32122px;
                    animation-delay: -14.2313s; animation-duration: 42.6845s; left: 6.51702vw;">
                </div>
                <div class="animated_shape circle speed-b" style="width: 6.54244px; height: 6.54244px;
                    animation-delay: -28.4294s; animation-duration: 40.5789s; left: 91.469vw;">
                </div>
                <div class="animated_shape circle speed-b" style="width: 9.04102px; height: 9.04102px;
                    animation-delay: -15.3254s; animation-duration: 41.9392s; left: 30.8222vw;">
                </div>
                <div class="animated_shape circle speed-b" style="width: 9.89844px; height: 9.89844px;
                    animation-delay: -18.1202s; animation-duration: 41.4251s; left: 67.5502vw;">
                </div>
                <div class="animated_shape speed-c" style="width: 3.58009px; height: 3.58009px; animation-delay: -20.8536s;
                    animation-duration: 38.2499s; left: 95.0655vw;">
                </div>
                <div class="animated_shape circle speed-c" style="width: 0.299453px; height: 0.299453px;
                    animation-delay: -41.64s; animation-duration: 38.1453s; left: 11.9843vw;">
                </div>
                <div class="animated_shape circle speed-b" style="width: 11.2674px; height: 11.2674px;
                    animation-delay: -35.3984s; animation-duration: 41.7953s; left: 37.6944vw;">
                </div>
                <div class="animated_shape speed-b" style="width: 5.04411px; height: 5.04411px; animation-delay: -8.25826s;
                    animation-duration: 42.5189s; left: 99.6939vw;">
                </div>
                <div class="animated_shape speed-b" style="width: 5.90774px; height: 5.90774px; animation-delay: -35.6433s;
                    animation-duration: 43.1995s; left: 75.211vw;">
                </div>
                <div class="animated_shape circle speed-a" style="width: 2.20847px; height: 2.20847px;
                    animation-delay: -30.8481s; animation-duration: 31.3005s; left: 38.1074vw;">
                </div>
                <div class="animated_shape speed-c" style="width: 5.8512px; height: 5.8512px; animation-delay: -14.634s;
                    animation-duration: 38.3522s; left: 10.1303vw;">
                </div>
                <div class="animated_shape circle speed-c" style="width: 1.32984px; height: 1.32984px;
                    animation-delay: -10.0213s; animation-duration: 36.002s; left: 69.7877vw;">
                </div>
                <div class="animated_shape circle speed-b" style="width: 4.19847px; height: 4.19847px;
                    animation-delay: -18.411s; animation-duration: 43.4295s; left: 60.8843vw;">
                </div>
                <div class="animated_shape circle speed-c" style="width: 10.0213px; height: 10.0213px;
                    animation-delay: -10.4328s; animation-duration: 35.4685s; left: 2.19797vw;">
                </div>
                <div class="animated_shape speed-c" style="width: 9.08942px; height: 9.08942px; animation-delay: -14.8069s;
                    animation-duration: 37.2199s; left: 7.5356vw;">
                </div>
                <div class="animated_shape speed-a" style="width: 2.91952px; height: 2.91952px; animation-delay: -42.095s;
                    animation-duration: 33.0235s; left: 26.8195vw;">
                </div>
                <div class="animated_shape speed-a" style="width: 7.38655px; height: 7.38655px; animation-delay: -31.0906s;
                    animation-duration: 30.894s; left: 14.6553vw;">
                </div>
                <div class="animated_shape circle speed-a" style="width: 1.77082px; height: 1.77082px;
                    animation-delay: -16.2878s; animation-duration: 32.8109s; left: 24.2566vw;">
                </div>
                <div class="animated_shape speed-c" style="width: 1.74375px; height: 1.74375px; animation-delay: -21.5518s;
                    animation-duration: 38.2776s; left: 61.1795vw;">
                </div>
                <div class="animated_shape circle speed-a" style="width: 3.2041px; height: 3.2041px;
                    animation-delay: -11.9295s; animation-duration: 32.8517s; left: 40.5612vw;">
                </div>
                <div class="animated_shape speed-b" style="width: 5.6797px; height: 5.6797px; animation-delay: -39.321s;
                    animation-duration: 42.2316s; left: 98.1279vw;">
                </div>
                <div class="animated_shape speed-a" style="width: 5.77346px; height: 5.77346px; animation-delay: -19.1104s;
                    animation-duration: 31.6668s; left: 68.4438vw;">
                </div>
                <div class="animated_shape circle speed-c" style="width: 0.406458px; height: 0.406458px;
                    animation-delay: -40.5825s; animation-duration: 37.2941s; left: 38.1699vw;">
                </div>
                <div class="animated_shape circle speed-c" style="width: 9.45384px; height: 9.45384px;
                    animation-delay: -5.34554s; animation-duration: 37.4379s; left: 52.2736vw;">
                </div>
                <div class="animated_shape speed-c" style="width: 0.856287px; height: 0.856287px;
                    animation-delay: -34.2079s; animation-duration: 36.4914s; left: 4.788vw;">
                </div>
                <div class="animated_shape speed-c" style="width: 1.05708px; height: 1.05708px; animation-delay: -23.8018s;
                    animation-duration: 35.6529s; left: 54.3834vw;">
                </div>
                <div class="animated_shape speed-b" style="width: 0.202036px; height: 0.202036px;
                    animation-delay: -36.251s; animation-duration: 44.4297s; left: 95.328vw;">
                </div>
                <div class="animated_shape circle speed-c" style="width: 1.13597px; height: 1.13597px;
                    animation-delay: -14.7003s; animation-duration: 39.6858s; left: 10.8156vw;">
                </div>
                <div class="animated_shape speed-a" style="width: 8.57622px; height: 8.57622px; animation-delay: -29.579s;
                    animation-duration: 31.4908s; left: 30.808vw;">
                </div>
                <div class="animated_shape circle speed-b" style="width: 1.52164px; height: 1.52164px;
                    animation-delay: -26.5749s; animation-duration: 40.4179s; left: 34.3064vw;">
                </div>
                <div class="animated_shape circle speed-b" style="width: 5.92394px; height: 5.92394px;
                    animation-delay: -19.2439s; animation-duration: 40.1162s; left: 40.3603vw;">
                </div>
                <div class="animated_shape circle speed-a" style="width: 0.610448px; height: 0.610448px;
                    animation-delay: -28.7596s; animation-duration: 34.0915s; left: 60.6301vw;">
                </div>
                <div class="animated_shape circle speed-b" style="width: 0.108338px; height: 0.108338px;
                    animation-delay: -42.5083s; animation-duration: 43.4147s; left: 34.1592vw;">
                </div>
                <div class="animated_shape circle speed-b" style="width: 0.878371px; height: 0.878371px;
                    animation-delay: -5.47127s; animation-duration: 44.2346s; left: 3.04322vw;">
                </div>
                <div class="animated_shape circle speed-a" style="width: 9.64988px; height: 9.64988px;
                    animation-delay: -27.5503s; animation-duration: 34.8777s; left: 41.9973vw;">
                </div>
                <div class="animated_shape circle speed-a" style="width: 10.1687px; height: 10.1687px;
                    animation-delay: -0.509976s; animation-duration: 32.0276s; left: 36.5656vw;">
                </div>
                <div class="animated_shape circle speed-a" style="width: 7.03873px; height: 7.03873px;
                    animation-delay: -0.511983s; animation-duration: 33.6964s; left: 68.1892vw;">
                </div>
                <div class="animated_shape speed-c" style="width: 1.07897px; height: 1.07897px; animation-delay: -0.165983s;
                    animation-duration: 37.942s; left: 93.2048vw;">
                </div>
                <div class="animated_shape speed-b" style="width: 0.741558px; height: 0.741558px;
                    animation-delay: -20.8526s; animation-duration: 40.0035s; left: 64.473vw;">
                </div>
            </div>
            <div class="center_box_container login_view skew_in_bottom">
                <div class="center_box white_box_with_shadow">
                    <div class="title">
                        Login to LMCC</div>
                    <div class="subtitle" style="margin-bottom: 20px;">
                        Trade Finance System
                    </div>
                    <table class="internal-login" style="width: 100%;">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" placeholder="UserName" class="input bottom-margin medium full_width"
                                    MaxLength="50" Width="229px"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnUserList" runat="server" CssClass="help_bt1" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" class="input bottom-margin medium full_width "
                                    MaxLength="50" TextMode="Password" autocomplete="off" Width="229px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 30px; padding-right: 25px">
                                <asp:Button ID="btnLogin" runat="server" Text="LogIn" class="button medium full_width"
                                    BackColor="#12294D" ToolTip="Login" OnClientClick="Submithashpass();"
                                    OnClick="btnLogin_Click" />  
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lbllocked" runat="server" Style="color: Red; font-size: small;" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="white_background light_background">
            </div>
            <div class="app_version_footer ">
                <div style="height: 15px;" align="center">
                    <span class="h1" style="font-weight: bold">&copy;&nbsp;2024 Lateral Management Computer
                        Consultants</span></div>
                <asp:Label ID="lbl1" runat="server" Text="" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
    </form>
        <script type="text/javascript">
            document.addEventListener('DOMContentLoaded', function () {
                var inputFields = document.querySelectorAll('input[type="text"], input[type="password"]');
                inputFields.forEach(function (inputField) {
                    inputField.addEventListener('copy', function (event) {
                        event.preventDefault();
                    });
                    inputField.addEventListener('paste', function (event) {
                        event.preventDefault();
                    });

                    // Intercept Ctrl+C and Ctrl+V key combinations
                    inputField.addEventListener('keydown', function (event) {
                        if ((event.ctrlKey || event.metaKey) && (event.key === 'c' || event.key === 'v')) {
                            event.preventDefault();
                        }
                    });
                });
            });
    </script>
</body>
</html>
