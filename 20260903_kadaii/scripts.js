let touchStartX = 0;
let touchStartY = 0;

const touchArea = document.getElementById("unity-container");
let coinCount = 0;

// WebGLのインスタンスを格納する変数
let unityInstance = null;

// WebGLを使用する準備を行う
// Unity画面の描画位置を決める
createUnityInstance(
    // 描画位置
    document.querySelector("#unity-canvas"), 
    // 描画に必要な設定
    {
        // data(3Dモデルなどアセット)
        dataUrl: "WEBGLBuild/Build/WEBGLBuild.data", 
        // faramework(jslibなどでビルドされたもの)
        frameworkUrl: "WEBGLBuild/Build/WEBGLBuild.framework.js",
        // wasm(コンパイルされたもの)
        codeUrl: "WEBGLBuild/Build/WEBGLBuild.wasm" 
    }
).then((instance) => {
    unityInstance = instance;
    console.log("Unityインスタンス設定完了")
}).catch((e) => {
    console.log("インスタンス設定エラー")
});

function CoinRespawn(){
            if(unityInstance){
                unityInstance.SendMessage(
                "GameManager", 
                "CoinRespawn"
                );
            }
        }

function ReceiveMessageFromUnity(message){
    if(message == "Coin"){
        coinCount++;

        document.getElementById("message-area").textContent =   "コイン: " + coinCount + "枚";
    }
}

// タッチ開始
touchArea.addEventListener("touchstart", function(e) {

    const touch = e.touches[0];

    touchStartX = touch.clientX;
    touchStartY = touch.clientY;

}, { passive: false });


// タッチ中
touchArea.addEventListener("touchmove", function(e) {

    e.preventDefault();

    const touch = e.touches[0];

    const currentX = touch.clientX;
    const currentY = touch.clientY;

    // 開始位置からどれだけ動いたか
    const deltaX = currentX - touchStartX;
    const deltaY = currentY - touchStartY;

    // タッチの移動量を正規化
    const length = Math.sqrt(
        deltaX * deltaX +
        deltaY * deltaY
    );

    if (length > 0) {

        const directionX = deltaX / length;

        // 画面上ではYが下方向なので反転
        const directionY = -deltaY / length;

        unityInstance.SendMessage(
            "Player",
            "SetTouchDirection",
            directionX + "," + directionY
        );
    }

}, { passive: false });


// タッチ終了
touchArea.addEventListener("touchend", function(e) {

    unityInstance.SendMessage(
        "Player",
        "StopTouch"
    );

});
