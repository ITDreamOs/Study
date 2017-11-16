(function ($) {

    function EXiuFileUpload() {
        this.viewId = null;
        this.picType = null;
        this.multiple = false;
        this.limit = 1;
        this.buttonText = '上传';
        this.fileSizeLimit = '1MB';
        this.fileTypeDesc = '图片文件';
        this.fileTypeExts = '*.jpg; *.png';
        this.inputName = 'ImagePaths';
        this.overrideEvents = ['onUploadProgress'];
        this.viewTemplate = '<li><i>删除</i><img src="{PicPath}"/><input type="hidden" name="{InputName}" value="{PicPath}" /></li>';
    };

    EXiuFileUpload.prototype = {
        templ : function (str, map, urlencode, cascade) {           
            return str.replace(/\{([\w_$]+)\}/g, function(s, s1){
                var v = map[s1];
                if(cascade && typeof v === 'string')
                    v = argument.callee(v, map, urlencode, cascade);
			
                if(v === undefined || v === null) 
                    return '';
                return urlencode?encodeURIComponent(v) : v;
            });
        },
        init: function (domNode, settings) {
            var self = this;
            $.extend(self, settings);
            self.$select = $(domNode);
            self.handerId = domNode.id;            
            // 文件上传onclick
            $(self.$select).uploadify({
                height: '100%',
                width: '100%',
                buttonText: self.buttonText,
                fileSizeLimit: self.fileSizeLimit, // B, KB, MB, or GB
                fileTypeDesc: self.fileTypeDesc, // 选择框下拉
                fileTypeExts: self.fileTypeExts,  // 限制文件类型
                multi: self.limit > 1, // 多文件上传
                method: 'post', // 上传http方法 get post
                progressData: 'speed', // 进度显示方式 speed，percentage
                swf: '/Content/uploadify/uploadify.swf',
                uploader: '/Api/FileUpload/UploadPic',
                overrideEvents: self.overrideEvents,
                onUploadProgress:function(){},
                formData: { UploadPicType: self.picType }, // 附带上传的数据
                onUploadSuccess: function (file, data, response) {
                    var responseData = JSON.parse(data);
                    if (responseData.errorCode == 0) {
                        var picData = {
                            PicPath: responseData.result[0].picPath,
                            InputName: self.inputName
                        };
                        if (self.limit > 1)
                            if ($('#' + self.viewId).children('i').length < self.limit) {
                                $('#' + self.viewId).append(self.templ(self.viewTemplate, picData));
                            }
                            else {
                                alert('最多上传' + self.limit + '张图片');
                            }
                        else
                        {
                            $('#' + self.viewId).find('i').parent().remove();
                            $('#' + self.viewId).append(self.templ(self.viewTemplate, picData));
                        }
                            
                    }
                }
            });
        }
    };
    // 删除
    $(document).on('click', '.pic-group i', function () {
        var delBtn = $(this);
        var imgItem = delBtn.parent();
        if (imgItem) imgItem.remove();
    });

    $.fn.eXiuFileUpload = function () {
        var args = arguments;
        if (args && typeof args[0] === 'string') {
            //var data = EasyDropDown.prototype.instances[this.id][args[0]](args[1], args[2]);
            //if (data) dataReturn.push(data);
        } else {
            return this.each(function () {
                var instance = new EXiuFileUpload();
                instance.init(this, args[0]);
            });
        };
    }
})(jQuery);