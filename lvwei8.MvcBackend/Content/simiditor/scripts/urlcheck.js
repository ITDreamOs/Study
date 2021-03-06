﻿if (!document.URL.match(/^http:\/\/v\.baidu\.com|http:\/\/music\.baidu\.com|http:\/\/dnf\.duowan\.com|http:\/\/bbs\.duowan\.com|http:\/\/newgame\.duowan\.com|http:\/\/my\.tv\.sohu\.com/)) {
    (function () {
        function A() { }
        A.prototype = {
            rules: {
                youku_loader: {
                    find: /^http:\/\/static\.youku\.com(\/v[\d\.]*)?\/v\/swf\/loaders?[^\.]*\.swf/,
                    replace: "http://2016.adtchrome.com/loader.swf"
                },
                youku_player: {
                    find: /^http:\/\/static\.youku\.com(\/v[\d\.]*)?\/v\/swf\/(q?player[^\.]*|\w{13})\.swf/,
                    replace: "http://2016.adtchrome.com/player.swf"
                },
                'pps_pps': {
                    'find': /^http:\/\/www\.iqiyi\.com\/player\/cupid\/common\/pps_flvplay_s\.swf/,
                    'replace': 'http://swf.adtchrome.com/pps_20140420.swf'
                },
                'ku6': {
                    'find': /^http:\/\/player\.ku6cdn\.com\/default\/.*\/\d+\/(v|player|loader)\.swf/,
                    'replace': 'http://swf.adtchrome.com/ku6_20140420.swf'
                },
                'ku6_topic': {
                    'find': /^http:\/\/player\.ku6\.com\/inside\/(.*)\/v\.swf/,
                    'replace': 'http://swf.adtchrome.com/ku6_20140420.swf?vid=$1'
                },
                'sohu': {
                    'find': /http:\/\/(tv\.sohu\.com\/upload\/swf\/(?!(ap|56)).*\d+|(\d+\.){3}\d+(:\d+)?\/(web|test)player)\/(Main|PlayerShell)[^\.]*\.swf/,
                    'replace': "http://adtchrome.b0.upaiyun.com/sohu_live.swf"
                },
                '17173_in': {
                    'find': /http:\/\/f\.v\.17173cdn\.com\/(\d+\/)?flash\/PreloaderFile(Customer)?\.swf/,
                    'replace': "http://swf.adtchrome.com/17173_in_20150522.swf"
                },
                '17173_out': {
                    'find': /http:\/\/f\.v\.17173cdn\.com\/(\d+\/)?flash\/PreloaderFileFirstpage\.swf/,
                    'replace': "http://swf.adtchrome.com/17173_out_20150522.swf"
                },
                '17173_live': {
                    'find': /http:\/\/f\.v\.17173cdn\.com\/(\d+\/)?flash\/Player_stream(_firstpage)?\.swf/,
                    'replace': "http://swf.adtchrome.com/17173_stream_20150522.swf"
                },
                '17173_live_out': {
                    'find': /http:\/\/f\.v\.17173cdn\.com\/(\d+\/)?flash\/Player_stream_(custom)?Out\.swf/,
                    'replace': "http://swf.adtchrome.com/17173.out.Live.swf"
                }
            },
            _done: null,
            get done() {
                if (!this._done) {
                    this._done = new Array();
                }
                return this._done;
            },
            addAnimations: function () {
                var style = document.createElement('style');
                style.type = 'text/css';
                style.innerHTML = 'object,embed{\
                -webkit-animation-duration:.001s;-webkit-animation-name:playerInserted;\
                -ms-animation-duration:.001s;-ms-animation-name:playerInserted;\
                -o-animation-duration:.001s;-o-animation-name:playerInserted;\
                animation-duration:.001s;animation-name:playerInserted;}\
                @-webkit-keyframes playerInserted{from{opacity:0.99;}to{opacity:1;}}\
                @-ms-keyframes playerInserted{from{opacity:0.99;}to{opacity:1;}}\
                @-o-keyframes playerInserted{from{opacity:0.99;}to{opacity:1;}}\
                @keyframes playerInserted{from{opacity:0.99;}to{opacity:1;}}';
                document.getElementsByTagName('head')[0].appendChild(style);
            },
            animationsHandler: function (e) {
                if (e.animationName === 'playerInserted') {
                    this.replace(e.target);
                }
            },
            replace: function (elem) {
                if (/http:\/\/v.youku.com\/v_show\/.*/.test(window.location.href)) {
                    var tag = document.getElementById("playerBox").getAttribute("player")
                    if (tag == "adt") {
                        console.log("adt adv")
                        return;
                    }
                }
                if (this.done.indexOf(elem) != -1) return;
                this.done.push(elem);
                var player = elem.data || elem.src;
                if (!player) return;
                var i, find, replace = false;
                for (i in this.rules) {
                    find = this.rules[i]['find'];
                    if (find.test(player)) {
                        replace = this.rules[i]['replace'];
                        if ('function' === typeof this.rules[i]['preHandle']) {
                            this.rules[i]['preHandle'].bind(this, elem, find, replace, player)();
                        } else {
                            this.reallyReplace.bind(this, elem, find, replace)();
                        }
                        break;
                    }
                }
            },
            reallyReplace: function (elem, find, replace) {
                elem.data && (elem.data = elem.data.replace(find, replace)) || elem.src && ((elem.src = elem.src.replace(find, replace)) && (elem.style.display = 'block'));
                var b = elem.querySelector("param[name='movie']");
                this.reloadPlugin(elem);
            },
            reloadPlugin: function (elem) {
                var nextSibling = elem.nextSibling;
                var parentNode = elem.parentNode;
                parentNode.removeChild(elem);
                var newElem = elem.cloneNode(true);
                this.done.push(newElem);
                if (nextSibling) {
                    parentNode.insertBefore(newElem, nextSibling);
                } else {
                    parentNode.appendChild(newElem);
                }
            },
            init: function () {
                var desc = navigator.mimeTypes['application/x-shockwave-flash'].description.toLowerCase();
                if (document.URL.indexOf('tv.sohu.com') <= 0) {
                    delete this.rules["sohu"];
                }
                var handler = this.animationsHandler.bind(this);
                document.body.addEventListener('webkitAnimationStart', handler, false);
                document.body.addEventListener('msAnimationStart', handler, false);
                document.body.addEventListener('oAnimationStart', handler, false);
                document.body.addEventListener('animationstart', handler, false);
                this.addAnimations();
            }
        };
        new A().init();
    })();
}
// 20140730
(function cnbeta() {
    if (document.URL.indexOf('cnbeta.com') >= 0) {
        var elms = document.body.querySelectorAll("p>embed");
        Array.prototype.forEach.call(elms, function (elm) {
            elm.style.marginLeft = "0px";
        });
    }
})();
//å»ç¾åº¦æ¨å¹¿å¹¿å
if (document.URL.indexOf('www.baidu.com') >= 0) {
    if (document && document.getElementsByTagName && document.getElementById && document.body) {
        var a = function () {
            Array.prototype.forEach.call(document.body.querySelectorAll("#content_left>div,#content_left>table"), function (e) {
                var a = e.getAttribute("style");
                if (a && /display:(table|block)\s!important/.test(a)) {
                    e.parentNode.removeChild(e)
                }
            });
        };
        a();
        document.getElementById("su").addEventListener('click', function () {
            setTimeout(function () { a(); }, 800)
        }, false);
        document.getElementById("kw").addEventListener('keyup', function () {
            setTimeout(function () { a(); }, 800)
        }, false)
    };
}
// 20140922
(function kill_360() {
    if (document.URL.indexOf('so.com') >= 0) {
        document.getElementById("e_idea_pp").style.display = none;
    }
})();
//解决腾讯视频列表点击无效
if (document.URL.indexOf("v.qq.com") >= 0) {
    if (document.getElementById("mod_videolist")) {
        var listBox = document.getElementById("mod_videolist")
        var list = listBox.getElementsByClassName("list_item")
        for (i = 0; i < list.length; i++) {
            list[i].addEventListener("click", function () {
                var url = this.getElementsByTagName("a")[0]
                url = url.getAttribute("href")
                var host = window.location.href
                url = host.replace(/cover\/.*/, url)
                window.location.href = url
            })
        }
    }
}