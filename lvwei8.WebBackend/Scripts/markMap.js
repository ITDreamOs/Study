(function ($) {

    function EXiuMarkMap() {        
        this.viewTemplate = [
        '<div class="modal" id="{modalId}" tabindex="-1" role="dialog" aria-hidden="true">',
        '    <div class="modal-dialog modal-lg">',
        '        <div class="modal-content">',
        '            <div class="modal-header">',
        '                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>',
        '                <h4 class="modal-title" id="myModalLabel">地图标注</h4>',
        '            </div>',
        '            <div class="modal-body">',
        '                <div id="{modalMapId}" class="map-container"></div>',
        '            </div>',
        '            <div class="modal-footer">',
        '                <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>',
        '            </div>',
        '        </div>',
        '    </div>',
        '</div>'].join('');
        this.markerPos = null;
    };

    EXiuMarkMap.prototype = {
        init: function (domNode, settings) {
            var self = this;
            $.extend(self, settings);
            self.$select = $(domNode);
            self.handerId = domNode.id;
            self.modalId = 'modal-' + self.handerId;
            self.modalMapId = 'modal-map-container-' + self.handerId;
            self.$select.click(function () {
                self.initView();
            });            
        },
        //初始化弹框
        initView: function () {
            var self = this;
            if (!$('#' + self.modalId).length > 0) {
                //创建地图加载框
                $('body').append(self.templ(self.viewTemplate, self));

                this.emapApi = new BMap.Map(self.modalMapId, { minZoom: 4, maxZoom: 17 });
                this.navigation = new BMap.NavigationControl({
                    anchor: BMAP_ANCHOR_TOP_LEFT,
                    type: BMAP_NAVIGATION_CONTROL_LARGE
                });
                this.emapApi.addControl(self.navigation);  //添加默认缩放平移控件
                this.emapApi.enableScrollWheelZoom();//启用地图滚轮放大缩小
                this.emapApi.enableKeyboard();//启用键盘上下左右键移动地图                

                if (this.markerPos && this.markerPos.lng && this.markerPos.lat) {
                    // 已标注位置
                    var point = new BMap.Point(this.markerPos.lng, this.markerPos.lat);
                    this.emapApi.centerAndZoom(point, 14); // 设置中心点坐标
                    this.addMarker(point, new BMap.Size(-46, -22));
                } else {
                    //根据百度获取当前城市
                    var baiduLocalCity = new BMap.LocalCity();
                    baiduLocalCity.get(function (result) {
                        cityName = result.name;
                        if (cityName) {
                            self.emapApi.centerAndZoom(cityName, 14);
                            self.city = cityName;
                        }
                    });
                }
                //地图点击事件
                this.emapApi.addEventListener("click", function (e) {
                    self.signMap(e);
                });
            }
            $('#' + self.modalId).modal('show');
        },
        //标注地图
        signMap: function (e) {
            var self = this;
            self.emapApi.clearOverlays();
            self.addMarker(e.point, new BMap.Size(-46, -22));
            self.callBack&&self.callBack(e.point.lat, e.point.lng);
        },
        //地图标注
        addMarker: function (point, off) {            
            var marker = new BMap.Marker(point, {
                icon: new BMap.Icon('http://app.baidu.com/map/images/us_mk_icon.png', new BMap.Size(24, 24), { imageOffset: off })
            });
            //地图标注
            this.emapApi.addOverlay(marker);
        },
        //模板替换方法
        templ : function (str, map, urlencode, cascade) {           
            return str.replace(/\{([\w_$]+)\}/g, function(s, s1){
                var v = map[s1];
                if(cascade && typeof v === 'string')
                    v = argument.callee(v, map, urlencode, cascade);
			
                if(v === undefined || v === null) 
                    return '';
                return urlencode?encodeURIComponent(v) : v;
            });
        }
    };

    $.fn.eXiuMarkMap = function () {
        var args = arguments;
        if (args && typeof args[0] === 'string') {            
        } else {
            return this.each(function () {
                var instance = new EXiuMarkMap();
                instance.init(this, args[0]);
            });
        };
    }
})(jQuery);