(function(window) {
    window["env"] = window["env"] || {};

    // Environment variables
    window["env"]["prod"] = false;
    window["env"]["demo"] = false;
    window["env"]["searchBotDisallow"] = false;
    window["env"]["supportEmail"] = "lk.support@nntu.ru";
    window["env"]["supportPhoneNumber"] = "220-15-94";
    window["env"]["maidenName"] = "Прежняя фамилия";
    window["env"]["orderSed"] = "Ссылка на документ в системе ТЕЗИС";
    window["env"]["fullHeader"] = "Нижегородский государственный технический университет им. Р.Е. Алексеева";
    window["env"]["shortHeader"] = "НГТУ им. Р.Е. Алексеева";
    window["env"]["invalidTokenMessage"] = "Вы пробуете зайти в личный кабинет работника. Личный кабинет студента расположен по ссылке";


    window["env"]["services"] = [];

    window["env"]["menuItems"] = [
        {text: "Зарплатный лист", icon:"k-i-calculator", url: "https://sal.nntu.ru/"},
        {text: "Официальный сайт НГТУ", icon:"k-i-hyperlink-open", url: "https://www.nntu.ru/"},
    ];

    window["env"]["announcement_maxFileSize"] = "20971520";
    window["env"]["announcement_commentMaxFileSize"] = "5242880";
    window["env"]["contingent_maxFileSize"] = "4194304";
    window["env"]["publications_maxFileSize"] = "10485760";
})(this);

