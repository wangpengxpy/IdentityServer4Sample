(function () {
    'use strict';

    var conference = window.conference || {};

    conference.clickEvent = (document.ontouchstart !== null) ? 'click' : 'touchstart';

    conference.getQueryValue = function (C, B) {
        var A = "";
        if (B) {
            A = "&" + B;
        } else {
            A = window.location.search.replace(/(^\?+)|(#\S*$)/g, "")
        }
        A = A.match(new RegExp("(^|&)" + C + "=([^&]*)(&|$)", 'i'));
        return !A ? "" : decodeURIComponent(A[2]);
    };

    // 返回相对应用程序根目录的路径。
    conference.href = function (url) {
        if (!url) return '';
        var appRoot = window.appRoot || '/';
        if (appRoot.substring(appRoot.length - 1) !== '/') {
            appRoot = appRoot + '/';
        }
        if (url.substring(0, 2) === '~/') {
            url = url.substring(2);
        } else if (url.substring(0, 1) === '/') {
            url = url.substring(1);
        } else if (url.substring(0, 7) === 'http://') {
            return url;
        }
        return appRoot + url;
    };

    /*
     * 使用 window.location.href 进行重定向。
     * 
     * @param {uri} url 表示重定向目标URL.
     * @returns 如果 url 为空，则使用 ReturnUrl 参数，否则重定向到 ‘/’。
     */
    conference.redirect = function (url) {
        location.href = (url || conference.getQueryValue('ReturnUrl') || '/');
    };
    /*
     * 使用 window.location.replace 进行重定向。
     * 
     * @param {uri} url 表示重定向目标URL.
     * @returns 如果 url 为空，则使用 ReturnUrl 参数，否则重定向到 ‘/’。
     */
    conference.redirectWithReplace = function (url) {
        location.replace((url || conference.getQueryValue('ReturnUrl') || '/'));
    };

    /*
	* conference.ajax
	*/
    (function () {

        //打开artdialog模态框
        conference.showDialogModal = function (content) {
            var modalDialog = dialog({
                content: content,
                zIndex: 20000
            });
            modalDialog.showModal();
            setTimeout(function () {
                modalDialog.close().remove();
            }, 2000);
        };

        //Ajax模态框加载
        var hideDialog = dialog({
            zIndex: 20000
        });

        conference.defaultAjaxSettings = {
            url: "#",
            method: "GET",
            //dataType: "JSON",
            data: "",
            timeout: 600000,
            async: true,
            beforeSend: function () {
                hideDialog.showModal();
            }
        };

        conference.ajax = function (ajaxSettings, successCallback, failCallback) {

            ajaxSettings = $.extend({}, this.defaultAjaxSettings, ajaxSettings);

            var d;

            return $.ajax(ajaxSettings).done(function (rsp) {
                successCallback && successCallback(rsp);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                var failData = {};
                if (jqXHR.responseText) {
                    try {
                        failData = JSON.parse(jqXHR.responseText);
                    } catch (e) {
                        failData = {};
                    }
                }

                var code = failData.code || jqXHR.statusCode(),
                    description = failData.description || (ajaxSettings.method + ' "' + ajaxSettings.url + '" ' + textStatus + ',' + errorThrown),
                    stackTrace = failData.stackTrace || '';

                d = dialog({
                    content: description,
                    zIndex: 20000
                });

                if (typeof (failCallback) === 'function') {
                    failCallback(code, description);
                }
                else {
                    d.showModal();
                }
            }).always(function () {
                setTimeout(function () {
                    d.close().remove();
                }, 2000);
                hideDialog.close().remove();
            });
        };
        /**
         * adjust the quick ajax extentions.
         * @param {string} method The first number.
         * @param {string} url The second number.
         * @param {Object} parameter The second number.
         * @param {Function} successCallback The second number.
         * @param {Function} failCallback The second number.
         * @returns {j} The sum of the two numbers.
         */
        function ajaxAdjust(method, url, parameter, successCallback, failCallback) {
            if (typeof (parameter) === 'function') {
                if (typeof (successCallback) === 'function') {
                    failCallback = successCallback;
                }
                successCallback = parameter;
                parameter = null;
            }
            url = conference.href(url);
            return conference.ajax({ url: url, method: method, data: parameter }, successCallback, failCallback);
        }
        //get请求数据json
        //conference.get(url,parameter)
        //conference.get(url,successCallback)
        //conference.get(url,successCallback,failCallback)
        //conference.get(url,parameter,successCallback,failCallback)
        conference.get = function (url, parameter, successCallback, failCallback) {
            return ajaxAdjust('GET', url, parameter, successCallback, failCallback);
        };
        //post请求数据json
        //conference.post(url,parameter)
        //conference.post(url,successCallback)
        //conference.post(url,successCallback,failCallback)
        //conference.post(url,parameter,successCallback,failCallback)
        conference.post = function (url, parameter, successCallback, failCallback) {
            return ajaxAdjust('POST', url, parameter, successCallback, failCallback);
        };
    }());

    // export
    window.conference = conference;
}());