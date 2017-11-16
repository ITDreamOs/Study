$(function () {

    var templ = function (str, map, urlencode, cascade) {
        return str.replace(/\{([\w_$]+)\}/g, function (s, s1) {
            var v = map[s1];
            if (cascade && typeof v === 'string')
                v = argument.callee(v, map, urlencode, cascade);

            if (v === undefined || v === null)
                return '';
            return urlencode ? encodeURIComponent(v) : v;
        });
    };

    $('[data-upload-role="fileUploadBtn"]').each(function () {
        // 参数
        var uploadPicType = $(this).attr('data-upload-pictype'),
            width = $(this).attr('data-upload-width') || '100%',
            height = $(this).attr('data-upload-height') || '100%',
            buttonText = $(this).attr('data-upload-buttontext') || '上传',
            isSingle = $(this).attr('data-upload-issingle'),
            fileSizeLimit = $(this).attr('data-upload-filesizelimit') || '1MB',
            fileTypeDesc = $(this).attr('data-upload-filetypedesc') || '图片文件',
            fileTypeExts = $(this).attr('data-upload-filetypeexts') || '*.jpg; *.png',
            overrideEvents = ($(this).attr('data-upload-overrideEvents') || '').split(',');
        // 进度条
        //var overrideEvents = [];
        var progressBarContainerId = $(this).attr('data-upload-progress');
        if (progressBarContainerId) {
            //overrideEvents.push('onUploadProgress');
        }
        // 隐藏域的名称
        var hiddenName = $(this).attr('data-upload-hiddenname');
        // 显示容器
        var imgContainer = $('#' + $(this).attr('data-upload-imgcontainer'));
        // 图片模板
        var imgItemTmpl = $('#' + $(this).attr('data-upload-imgitemtmpl')).html();

        var fileUploadBtn = $(this).uploadify({
            height: height,
            width: width,
            buttonText: buttonText,
            fileSizeLimit: fileSizeLimit, // B, KB, MB, or GB
            fileTypeDesc: fileTypeDesc, // 选择框下拉
            fileTypeExts: fileTypeExts,  // 限制文件类型
            multi: true, // 多文件上传
            method: 'post', // 上传http方法 get post
            progressData: 'speed', // 进度显示方式 speed，percentage
            swf: '/Content/uploadify/uploadify.swf',
            uploader: '/Api/FileUpload/UploadPic',
            overrideEvents : overrideEvents,
            formData: { UploadPicType: uploadPicType }, // 附带上传的数据
            onUploadSuccess: function (file, data, response) {
                var responseData = JSON.parse(data);
                if (responseData.errorCode == 0) {
                    var pciData = {
                        PicPath: responseData.result[0].picPath,
                        HiddenName: hiddenName
                    };
                    var imgItemHtml = templ(imgItemTmpl, pciData);
                    var hasImgItem = imgContainer.find('[data-upload-role="imgitem"]').length > 0;
                    if (isSingle && hasImgItem) {
                        // 单图，则替换
                        imgContainer.find('[data-upload-role="imgitem"] img').attr('src', pciData.PicPath);
                        // 修改隐藏域值
                        imgContainer.find('[data-upload-role="imgitem"] input[type="hidden"]').val(pciData.PicPath);
                    } else {
                        imgContainer.append(imgItemHtml);
                    }
                }
            },
            onUploadProgress : function(file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal) {
                var progressBarContainer = $('#' + progressBarContainerId);
                if (progressBarContainer) {

                }
            },

        });

        // 删除
        $(document).on('click', '[data-upload-role="imgitemdelbtn"]', function () {
            var delBtn = $(this);
            var imgItem = delBtn.parents('[data-upload-role="imgitem"]');
            if (imgItem) imgItem.remove();
        });
    });
});