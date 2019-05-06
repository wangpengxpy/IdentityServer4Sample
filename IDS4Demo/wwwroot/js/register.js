(function (window, $) {
    "use strict";

    var dom = {
        registerModal: $('#registerModal'),
        UserName: $('#userName'),
        Password: $('#Password'),
        confirmPassword: $('#ConfirmPassword'),
        Submit: $('#register')
    };

    var g = {
        registerUrl: 'account/register',
        messages: {
            RequiredUserName: '请输入注册账号',
            RequiredPassword: '请输入密码',
            RequiredPasswordLength: '请确保输入密码至少6位',
            RequiredConfirmPassword: '请输入确认密码',
            MatchPasswordError: '两次输入密码不一致'
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
        if (pwd.length < 6) {
            conference.showDialogModal(g.messages.RequiredPasswordLength);
            dom.Password.focus();
            return false;
        }
        return pwd;
    }

    function _getConfirmPassword() {
        var pwd = dom.confirmPassword.val();
        if (!pwd || pwd.length === 0) {
            conference.showDialogModal(g.messages.RequiredConfirmPassword);
            dom.confirmPassword.focus();
            return false;
        }
        return pwd;
    }

    function register(e) {
        e.preventDefault();
        var userName = _getUserName();
        if (!userName) { return false; }
        var password = _getPassword();
        if (!password) { return false; }
        var confirmPassword = _getConfirmPassword();
        if (!confirmPassword) { return false; }
        if (password !== confirmPassword) {
            conference.showDialogModal(g.messages.MatchPasswordError);
            return false;
        }

        var model = {
            username: userName,
            password: password,
            confirmPassword: confirmPassword,
            __RequestVerificationToken: _getVerifyToken()
        };
        userRegister(model);
        return false;
    }

    function userRegister(model) {
        conference.post(g.registerUrl, model, function (ret) {
            conference.redirectWithReplace();
        });
    }

    $(function () {
        dom.registerModal.modal('show');
    });

    dom.Submit.on('click', register);

    function _getVerifyToken() {
        return $('input[type=hidden][name="__RequestVerificationToken"]').val()
    }
}(this, jQuery));