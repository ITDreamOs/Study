+function ($) {
    'use strict';

    var hasFileAPISupport = function () {
        return window.File && window.FileReader && window.FileList && window.Blob;
    };

    // 定义上传类

    var FileInput = function (element, options) {
        this.$element = $(element);

        if (hasFileAPISupport()) {
            this.init(options);
            this.listen();
        }
    };

    FileInput.prototype = {
        constructor: FileInput,

        init: function (options) {
            var self = this;
            self.maxSize = options.maxSize;
            self.allowTypes = options.allowTypes;
            self.uploadUrl = options.uploadUrl;
            self.fileFormName = options.fileFormName;
            self.ajaxTimeout = options.ajaxTimeout;
            self.defaultImg = options.defaultImg;
            self.ERROR_TYPE = 1;
            self.ERROR_MAX_SIZE = 2;
            self.ERROR_MAX_COUNT = 3;
            self.ERROR_NETWORK = 4;
            self.ERROR_SERVER = 5;
            self.ERROR_RESPONSE = 6;
            self.ERROR_COMPRESS = 7;
            self.ERROR_TIMEOUT = 8;
            self.STATUS_SUCCESS = 0;
            self.STATUS_UPLOADING = 1;
            self.STATUS_ERROR = -1;
            self.STATUS_CANCEL = -2;
            self.STATUS_DELETE = -3;
        },

        listen: function () {
            var self = this, 
                $el = self.$element;
            $el.on('change', $.proxy(self.change, self));
            $el.closest('[data-role=images]').on("click", '[data-upload-role="imgitem"]',function(e){
                e.preventDefault();
                var i = this,
                    t = $(e.currentTarget),
                    n = t.find("img").attr("src");
                self.confirm(n,t);
            })
        },

        change: function (e) {
            var self = this,n = null,
                jq = self.$element,
                jqform = $(document).find("form[name='"+self.fileFormName+"']"),
                fn = jq.get(0).files;
            if (n = this.checkImg(fn)){
                return self.onError(n),!1;
            }
            self.PicInput = $(e.target);
            self.$addfile = $(e.target).closest("[data-role=addfile]");
            self.$images = $(self.$addfile).closest("[data-role=images]");
            self.$uploadType = $(e.target).attr("data-role");
            self.$maxCount = $(e.target).attr("data-max-count")
            self.$input = $(e.target).attr("data-upload-hiddenname");
            self.PID = self.generateID();

            this.onUploadLoad();
            this.getUploader(fn);
        },

        onUploadLoad: function(){
            var self = this;

            if (self.$maxCount != undefined || self.$maxCount != null){
                var o = self.$images.find("li").length - 1;
                if(o >= self.$maxCount){
                    return self.onError({code:self.ERROR_MAX_COUNT}),!1;
                }
            }
            if(self.$uploadType === "single"){
                self.$images.find('li[data-upload-role=imgitem]').remove();
            }
            var tpl = $('<li data-upload-role="imgitem" data-id="' + self.PID + '"></li>');
            var img = $("<img>");
                img.attr("src", self.defaultImg),
                tpl.append(img).addClass("loading"),
                tpl.addClass("loading");
            self.$addfile.after(tpl);
        },

        getUploader: function(e){
            var r, s, self = this;
            r = new XMLHttpRequest;
            s = new FormData(document.forms.namedItem(self.fileFormName));
            s.append("UploadPicType","1")
            r.addEventListener("load",function(e) {
                var dataPic;
                try {
                    dataPic = JSON.parse(e.target.responseText);
                } catch(l) {
                    return self.onError({code:self.ERROR_RESPONSE}),!1;
                }
                if (dataPic.ErrorMessage) {
                    return self.onError({code:self.ERROR_RESPONSE}),!1;
                }else {
                    s = dataPic.Results[0];
                    self.success(s);
                }
            },!1),
            r.addEventListener("error",function() {
                return self.onError({code:self.ERROR_RESPONSE}),!1;
            },!1),
            r.open("POST", self.uploadUrl, !0);
            r.send(s);
            self.PicInput.val("");
        },

        success: function(e) {
            var self = this;
            var i = self.$images.find('[data-id="' + self.PID + '"]');
            var t = i.find("img");
            t.attr("src", e.PicPath);
            i.removeClass("loading");
            self.saveFile(e,t);
        },

        saveFile: function(e,t) {
            var self = this;
            var i = $('<input type="hidden" name="'+self.$input+'" value="'+e.PicPath+'">');
            i.insertAfter(t);
            if(self.$uploadType === "multiple"){
                self.updateCountInfo();
            }
        },

        updateCountInfo: function() {
            var self = this;
            var o = self.$images.find("li").length;
            self.$hiddenCount = self.$images.next("[data-role=more]").find("[data-role=hiddenCount]");
            self.$hiddenCount.text(o - 4);
            o > 4 ? self.$images.next("[data-role=more]").show() : (self.$images.parent().removeClass("active"), self.$images.next("[data-role=more]").hide())
        },

        onError: function(i) {
            var msg = "", self = this;
            switch (i.code) {
            case self.ERROR_TYPE:
                msg = "文件类型错误";
                break;
            case self.ERROR_MAX_SIZE:
                msg = "文件太大了";
                break;
            case self.ERROR_MAX_COUNT:
                msg = "文件数量超过限制";
                break;
            case self.ERROR_SERVER:
            case self.ERROR_RESPONSE:
                msg = "服务器错误，请稍后再试";
                break;
            default:
                msg = "上传出错了"
            }
            self.tip(msg, 1500);
        },

        generateID: function() {
            var x = (new Date).getTime(),
            e = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g,
            function(e) {
                var n = (x + 16 * Math.random()) % 16 | 0;
                return x = Math.floor(x / 16),
                ("x" == e ? n: 7 & n | 8).toString(16)
            });
            return e
        },

        getfileExt: function (e) {
            var t = /\.(\w*)$/,
            i = e.match(t);
            return i ? i[1] : ""
        },

        checkImg: function(files) {
            var self = this,
                file = files[0],
                fileExt = this.getfileExt(file.name.toLowerCase()),
                fileSize = (file.size ? file.size : 0) / 1000,
                err = {};
            return self.allowTypes && -1 === self.allowTypes.indexOf(fileExt) ? (err.code = self.ERROR_TYPE,err) : self.maxSize && fileSize && fileSize > self.maxSize ? (err.code = self.ERROR_MAX_SIZE,err): 0;
        },

        tip: function(o, n) {
            var i, t = $('<div class="mtip"></div>').hide().appendTo("body");
            return o && t.html(o).show(),
            n && (clearTimeout(i), i = setTimeout(function() {
                t.hide()
            },
            n)),
            $("body").append(t),
            {
                setMessage: function(o, e) {
                    t.html(o),
                    e && (clearTimeout(i), i = setTimeout(function() {
                        t.remove()
                    },
                    e))
                },
                remove: function() {
                    t.remove()
                }
            }
        },

        parse: function(htmls, map) {
            var self = this;
            var tplRegIf = /\[\?(!?)\.([\w_$]+?)\?([\S\s]*?)\?\]/g,
                tplReg = /\{(\.?[\w_$]+)\}/g;

            if (!map)
                map = {};

            htmls = htmls.replace(tplRegIf, function(s, s0, s1, s2) {
                if (s0 === '!')
                    return !map[s1] ? s2 : '';

                return map[s1] === undefined ? '' : s2;
            });

            return htmls.replace(tplReg, function(s, k) {
                var v = k.charAt(0) === '.' ? map[k.substr(1)] : T.tpls[k];
                if (v === undefined || v === null)
                    return '';

                // html text
                if (v.toString().charAt(0) === '<')
                    return self.parse(v, map);

                return v;
            });
        },

        confirm: function (e, i) {
            var self = this;
            var dlgHtml = '<div style="display:block;" class="popup popup-delete"><div class="popup-head">删除照片</div><div class="popup-body"><p><img src="{.picData}"/></p><p>要删除这张图片吗？</p></div><div class="popup-bar"><label class="btn-group"><button class="btn btn-red btn-large" data-role="confirm">删除</button></label><label class="btn-group"><button class="btn btn-sub btn-large" data-role="cancel">返回</button></label></div></div><div style="display:block;" class="popup-mask"></div>';

            var assigns = {picData: e};
            
            var $show = $(self.parse(dlgHtml, assigns)).appendTo("body");
            $show.on("click", '[data-role="confirm"]',function(e) {
                self.$images = i.closest('[data-role=images]');
                i.remove();
                self.updateCountInfo();
                $show.remove();
            }).on("click", '[data-role="cancel"]',function(e) {
                $show.remove();
            });
        }
        
    }

    $.fn.fileinput = function (option) {
        if (!hasFileAPISupport()) {
          return;
        }
        
        var args = Array.apply(null, arguments);
        args.shift();
        return this.each(function () {
            var $this = $(this),
                data = $this.data('fileinput'),
                options = typeof option === 'object' && option;

            if (!data) {
                $this.data('fileinput',
                    (data = new FileInput(this, $.extend({}, $.fn.fileinput.defaults, options, $(this).data()))));
            }

            if (typeof option === 'string') {
                data[option].apply(data, args);
            }
        });
    };

    $.fn.fileinput.defaults = {
        maxSize : 2048,
        allowTypes : ["jpg", "jpeg", "bmp", "gif", "png"],
        uploadUrl: "/FileUpload/Upload",
        ajaxTimeout: 30,
        fileFormName:'pub-form',
        addfile: 'addfile',
        defaultImg: '/Content/images/nopic.gif'
    }

    $(document).ready(function () {
        var $input = $('input[type=file]'), count = $input.attr('type') != null ? $input.length : 0;
        if (count > 0) {
            $input.fileinput();
        }
    });

}(jQuery);
