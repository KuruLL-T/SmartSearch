(function(window) {
    window.env = window.env || {};

    // Environment variables
    window["env"]["prod"] = "${PROD}";
    window["env"]["demo"] = "${DEMO}";
    window["env"]["searchBotDisallow"] = "${SearchBot_Disallow}";
    window["env"]["apiUrl"] = "${API_URL}";
    window["env"]["authUrl"] = "${AUTH_URL}";
    window["env"]["allowedDomains"] = "${ALLOWED_DOMAINS}";
    window["env"]["apiUrlLk"] = "${API_URL_LK}";
    window["env"]["apiUrlFile"] = "${API_URL_FILE}";
    window["env"]["clientId"] = "${CLIENT_ID}";
    window["env"]["supportPhoneNumber"] = "${SUPPORT_PHONE_NUMBER}";
    window["env"]["supportEmail"] = "${SUPPORT_EMAIL}";
    window["env"]["maidenName"] = "${MAIDEN_NAME}";
    window["env"]["orderSed"] = "${ORDER_SED}";
    window["env"]["fullHeader"] = "${FULL_HEADER}";
    window["env"]["shortHeader"] = "${SHORT_HEADER}";
    window["env"]["remoteEntryTemplateURL"] = "${REMOTE_ENTRY_TEMPLATE_URL}";
    window["env"]["invalidTokenMessage"] = "${INVALID_TOKEN_MESSAGE}";
    window["env"]["services"] = parseEnvironmentItems("${Services}");
    window["env"]["menuItems"] = parseEnvironmentItems("${Menu_Items}");
    window["env"]["announcement_maxFileSize"] = "${announcement_maxFileSize}";
    window["env"]["announcement_commentMaxFileSize"] = "${announcement_commentMaxFileSize}";
    window["env"]["contingent_maxFileSize"] = "${contingent_maxFileSize}";
    window["env"]["publications_maxFileSize"] = "${publications_maxFileSize}";
    window["env"]["publications_maxFileSize"] = "10485760";

    window["env"]["loginGenerationFirst"] = "${LOGIN_GENERATION_FIRST}";
    window["env"]["loginGenerationSecond"] = "${LOGIN_GENERATION_SECOND}";
    window["env"]["studNumberGenerationFirst"] = "${STUD_NUMBER_GENERATION_FIRST}";
    window["env"]["studNumberGenerationSecond"] = "${STUD_NUMBER_GENERATION_SECOND}";

    window["env"]["cryptoArmMailButtonEnabled"] = "${CRYPTOARM_MAIL_BUTTON_ENABLED}" === 'true';
    window["env"]["cryptoArmAppMissingModalLayout"] = "${CRYPTOARM_APP_MISSING_MODAL_LAYOUT}";
})(this);

function parseEnvironmentItems(template) {
    let items = [];
    template.split(';').forEach((item) => {
        const [key, value] = item.trimStart().split('=');
        const [field, index] = key.split('__');
        items[index] = {...items[index], [field.toLowerCase()]: value};
    });
    return items;
}