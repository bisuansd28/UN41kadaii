mergeInto(LibraryManager.library, {
    SendMessageToJS: function(message){
        var msg = UTF8ToString(message);
        console.log(msg);

        ReceiveMessageFromUnity(msg);
    }
});