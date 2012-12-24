
var App = {}

App.deal = function () {
    App.connection.send('deal');
};

App.hit = function () {
    App.connection.send('hit');
};

App.stand = function () {
    App.connection.send('stand');
};

App.getSuitHtml = function (suit) {
    var image = 'club.png';
    if (suit === 'H') {
        image = 'heart.png';
    } else if (suit === 'S') {
        image = 'spade.png';
    } else if (suit === 'D') {
        image = 'diamond.png';
    }
    return "<img class='card' src='Images/" + image + "'/>";
};

App.getRankHtml = function (rank) {
    if (rank === 1) {
        return 'A';
    } else if (rank === 11) {
        return 'J';
    } else if (rank === 12) {
        return 'Q';
    } else if (rank === 13) {
        return 'K';
    }
    return rank;
};

App.getCardsHtml = function (cards) {
    var html = '';
    for (var i = 0; i < cards.length; i++) {
        var card = cards[i];
        html += App.getRankHtml(card.rank);
        html += App.getSuitHtml(card.suit);
    }
    return html;
};

App.updatePlayer = function (player) {
    var html = App.getCardsHtml(player.cards);
    $('#playerCards').html(html);
    $('#playerScore').text(player.score);
};

App.updateDealer = function (dealer) {
    var html = App.getCardsHtml(dealer.cards);
    $('#dealerCards').html(html);
    $('#dealerScore').text(dealer.score);
};

App.updateResult = function (result) {
    var displayResult = result;
    if (result === 'None') {
        displayResult = '';
    }
    $('#result').text(displayResult);
};

App.disableButton = function (id) {
    $(id).attr('disabled', 'disabled');
};

App.enableButton = function (id) {
    $(id).removeAttr('disabled');
};

App.disableDeal = function () {
    App.disableButton('#deal');
    App.enableButton('#hit');
    App.enableButton('#stand');
};

App.enableDeal = function () {
    App.enableButton('#deal');
    App.disableButton('#hit');
    App.disableButton('#stand');
};

App.enableDealIfGameFinished = function (result) {
    if (result !== 'None') {
        App.enableDeal();
    }
};

App.stringStartsWith = function (text, startsWith) {
    return text.slice(0, startsWith.length) === startsWith;
};

App.parseServerMessage = function (data) {
    var type = App.getServerMessageType(data);
    var index = data.indexOf('{');
    if ((index === -1) || (type === 'error')) {
        return { type: 'error', game: {} };
    }

    var gameJson = data.slice(index);
    var game = jQuery.parseJSON(gameJson);

    return {
        type: type,
        game: game
    };
};

App.getServerMessageType = function (data) {
    var type = 'error';
    if (App.stringStartsWith(data, 'stand')) {
        type = 'stand';
    } else if (App.stringStartsWith(data, 'deal')) {
        type = 'deal';
    } else if (App.stringStartsWith(data, 'hit')) {
        type = 'hit';
    }
    return type;
};

App.gameResult = function (data) {
    var message = App.parseServerMessage(data);
    if (message.type === 'stand') {
        App.standResult(message.game);
    } else if (message.type === 'deal') {
        App.dealResult(message.game);
    } else if (message.type === 'hit') {
        App.hitResult(message.game);
    }
};

App.dealResult = function (game) {
    App.disableDeal();
    App.updateDealer(game.dealer);
    App.updatePlayer(game.player);
    App.updateResult(game.result);
};

App.hitResult = function (game) {
    App.updateDealer(game.dealer);
    App.updatePlayer(game.player);
    App.updateResult(game.result);
    App.enableDealIfGameFinished(game.result);
};

App.standResult = function (game) {
    App.updateDealer(game.dealer);
    App.updatePlayer(game.player);
    App.updateResult(game.result);
    App.enableDealIfGameFinished(game.result);
};

App.connection = {}

App.registerClientActions = function () {

    $('#deal').click(function () {
        App.deal();
    });

    $('#hit').click(function () {
        App.hit();
    });

    $('#stand').click(function () {
        App.stand();
    });
};

App.registerServerActions = function () {
    App.connection.received(function (data) {
        App.gameResult(data);
    });
};

App.init = function () {
    var connection = $.connection('/signalr');
    App.connection = connection;
    connection.start();

    App.registerClientActions();
    App.registerServerActions();
    App.enableDeal();
};

$(document).ready(function () {
    App.init();
});