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
        showSecond: true, //��ʾ��
        timeFormat: 'hh:mm:ss', //��ʽ��ʱ��
        stepHour: 2, //���ò���
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



//��ȡҳ����ѡ�е�checkbox�е�ֵ������id��һ����

function GetIds() {
    var ids = [];
    $("tbody").find("input[name='ids']").each(function () {
        if ($(this).attr("checked")) {
            ids.push($(this).val());
        }
    });
    return ids;
}

//��ʾ��Ϣ��
//��һ����Ϊ��ʾ���ݣ����Ҫ���У�����html��ǩ
//�ڶ�������Ϊ��ʽ����Ĭ����success�����С��ο�cs�е�ö��GameCommon.Enum.Auth.ShowMsgStyle����information,attention,error
function Msg() {
    var obj = $(".content-box").prev().html();
    if(null != obj)$(".content-box").prev().remove();
    $(".content-box").before('<div class="notification ' + (undefined == arguments[1] ? 'success' : arguments[1]) + ' png_bg"><div>' + arguments[0] + '</div></div>');
    scroll(0, 0);
}