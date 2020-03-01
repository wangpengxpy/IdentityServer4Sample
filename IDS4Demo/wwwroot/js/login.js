(function (window, $) {
    "use strict";

    var dom = {
        loginModal: $('#loginModal'),
        UserName: $('#userName'),
        Password: $('#Password'),
        Submit: $('#login'),
        Register: $('#register')
    };

    var g = {
        loginUrl: 'account/login',
        messages: {
            RequiredUserName: '请输入登录账号',
            RequiredPassword: '请输入密码'
        }
    };

    function _getUserName() {
        var username = dom.UserName.val();
        if (!username || username.length === 0) {
            conference.showDialogModal(g.messages.RequiredUserName);
            dom.UserName.focus();
            return false;
        }
        return username;
    }

    function _getPassword() {
        var pwd = dom.Password.val();
        if (!pwd || pwd.length === 0) {      
            conference.showDialogModal(g.messages.RequiredPassword);
            dom.Password.focus();
            return false;
        }
        return pwd;
    }

    function login(e) {
        e.preventDefault();
        var userName = _getUserName();
        if (!userName) { return false; }
        var password = _getPassword();
        if (!password) { return false; }

        var model = {
            username: userName,
            password: password,
            __RequestVerificationToken: _getVerifyToken()
        };
        userLogin(model);
        return false;
    }

    function userLogin(model) {
        conference.post(g.loginUrl, model, function (ret) {
            conference.redirectWithReplace();
        });
    }

    $(function () {
        dom.loginModal.modal('show');
    });

    dom.Submit.on('click', login);

    function _getVerifyToken() {
        return $('input[type=hidden][name="__RequestVerificationToken"]').val();
    }

    dom.Register.on('click', function (e) {
        conference.redirect('/account/register');
    });
}(this, jQuery));