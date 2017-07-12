$(document).ready(function () {
    var cookieName = 'sungerAccordion';
    $('#main-nav').accordion({
        autoHeight: false,
        active: ($.cookies.get(cookieName) || 0),
        change: function (e, ui) {
            $.cookies.set(cookieName, $(this).find('h3').index(ui.newHeader[0]));
        }
    });

    $("input.datepicker").datetimepicker({
        showSecond: true, //显示秒
        timeFormat: 'hh:mm:ss', //格式化时间
        stepHour: 2, //设置步长
        stepMinute: 10,
        stepSecond: 10
    });

    $('tbody tr:even').addClass("alt-row"); // Add class "alt-row" to even table rows
    $('.check-all').click(
			function () {
			    $(this).parent().parent().parent().parent().find("input[type='checkbox']").attr('checked', $(this).is(':checked'));
			}
		);
});



//获取页面中选中的checkbox中的值，返回id的一数组

function GetIds() {
    var ids = [];
    $("tbody").find("input[name='ids']").each(function () {
        if ($(this).attr("checked")) {
            ids.push($(this).val());
        }
    });
    return ids;
}

//提示信息：
//第一参数为提示内容，如果要分行，请用html标签
//第二个参数为样式名，默认是success，还有【参考cs中的枚举GameCommon.Enum.Auth.ShowMsgStyle】：information,attention,error
function Msg() {
    var obj = $(".content-box").prev().html();
    if(null != obj)$(".content-box").prev().remove();
    $(".content-box").before('<div class="notification ' + (undefined == arguments[1] ? 'success' : arguments[1]) + ' png_bg"><div>' + arguments[0] + '</div></div>');
    scroll(0, 0);
}