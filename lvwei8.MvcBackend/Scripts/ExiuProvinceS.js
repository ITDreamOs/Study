var ExiuProvinces = {
    AllProvinces: [
	{
	    "Name": "北京市",
	    "Code": "110000",
	    "cityLevel": "1",
	    "Spell": "beijing"
	},
	{
	    "Name": "天津市",
	    "Code": "120000",
	    "cityLevel": "1",
	    "Spell": "tianjin"
	},
	{
	    "Name": "河北省",
	    "Code": "130000",
	    "cityLevel": "1",
	    "Spell": "hebei"
	},
	{
	    "Name": "山西省",
	    "Code": "140000",
	    "cityLevel": "1",
	    "Spell": "shanxi"
	},
	{
	    "Name": "内蒙古自治区",
	    "Code": "150000",
	    "cityLevel": "1",
	    "Spell": "neimenggu"
	},
	{
	    "Name": "辽宁省",
	    "Code": "210000",
	    "cityLevel": "1",
	    "Spell": "liaoning"
	},
	{
	    "Name": "吉林省",
	    "Code": "220000",
	    "cityLevel": "1",
	    "Spell": "jilin"
	},
	{
	    "Name": "黑龙江省",
	    "Code": "230000",
	    "cityLevel": "1",
	    "Spell": "heilongjiang"
	},
	{
	    "Name": "上海市",
	    "Code": "310000",
	    "cityLevel": "1",
	    "Spell": "shanghai"
	},
	{
	    "Name": "江苏省",
	    "Code": "320000",
	    "cityLevel": "1",
	    "Spell": "jiangsu"
	},
	{
	    "Name": "浙江省",
	    "Code": "330000",
	    "cityLevel": "1",
	    "Spell": "zhejiang"
	},
	{
	    "Name": "安徽省",
	    "Code": "340000",
	    "cityLevel": "1",
	    "Spell": "anhui"
	},
	{
	    "Name": "福建省",
	    "Code": "350000",
	    "cityLevel": "1",
	    "Spell": "fujian"
	},
	{
	    "Name": "江西省",
	    "Code": "360000",
	    "cityLevel": "1",
	    "Spell": "jiangxi"
	},
	{
	    "Name": "山东省",
	    "Code": "370000",
	    "cityLevel": "1",
	    "Spell": "shandong"
	},
	{
	    "Name": "河南省",
	    "Code": "410000",
	    "cityLevel": "1",
	    "Spell": "henan"
	},
	{
	    "Name": "湖北省",
	    "Code": "420000",
	    "cityLevel": "1",
	    "Spell": "hubei"
	},
	{
	    "Name": "湖南省",
	    "Code": "430000",
	    "cityLevel": "1",
	    "Spell": "hunan"
	},
	{
	    "Name": "广东省",
	    "Code": "440000",
	    "cityLevel": "1",
	    "Spell": "guangdong"
	},
	{
	    "Name": "广西壮族自治区",
	    "Code": "450000",
	    "cityLevel": "1",
	    "Spell": "guangxi"
	},
	{
	    "Name": "海南省",
	    "Code": "460000",
	    "cityLevel": "1",
	    "Spell": "hainan"
	},
	{
	    "Name": "重庆市",
	    "Code": "500000",
	    "cityLevel": "1",
	    "Spell": "chongqing"
	},
	{
	    "Name": "四川省",
	    "Code": "510000",
	    "cityLevel": "1",
	    "Spell": "sichuan"
	},
	{
	    "Name": "贵州省",
	    "Code": "520000",
	    "cityLevel": "1",
	    "Spell": "guizhou"
	},
	{
	    "Name": "云南省",
	    "Code": "530000",
	    "cityLevel": "1",
	    "Spell": "yunnan"
	},
	{
	    "Name": "西藏自治区",
	    "Code": "540000",
	    "cityLevel": "1",
	    "Spell": "xizang"
	},
	{
	    "Name": "陕西省",
	    "Code": "610000",
	    "cityLevel": "1",
	    "Spell": "shanxi1"
	},
	{
	    "Name": "甘肃省",
	    "Code": "620000",
	    "cityLevel": "1",
	    "Spell": "gansu"
	},
	{
	    "Name": "青海省",
	    "Code": "630000",
	    "cityLevel": "1",
	    "Spell": "qinghai"
	},
	{
	    "Name": "宁夏回族自治区",
	    "Code": "640000",
	    "cityLevel": "1",
	    "Spell": "ningxia"
	},
	{
	    "Name": "新疆维吾尔自治区",
	    "Code": "650000",
	    "cityLevel": "1",
	    "Spell": "xinjiang"
	}
    ],
    GetModel: function (name) {
        var provinces = this.AllProvinces;
        if (name == "") {
            return null;
        }
        if (provinces.length == 0) {
            return null;
        }
       
        var selectareas = [];
        for (var i = 0; i < provinces.length; i++) {
            var areamodel = provinces[i];
            if (areamodel.Name.indexOf(name)>=0) {
                selectareas.push(areamodel);
                break;
            }
        }
        if (selectareas.length == 0) {
            return null;
        }
        return selectareas[0];
    },
    GetModelByCode: function (code) {
        var provinces = this.AllProvinces;
        if (code == "") {
            return null;
        }
        if (provinces.length == 0) {
            return null;
        }

        var selectareas = [];
        for (var i = 0; i < provinces.length; i++) {
            var areamodel = provinces[i];
            if (areamodel.Code== code) {
                selectareas.push(areamodel);
                break;
            }
        }
        if (selectareas.length == 0) {
            return null;
        }
        return selectareas[0];
    }

}
