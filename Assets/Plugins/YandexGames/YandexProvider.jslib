mergeInto(LibraryManager.library, {

  Authenticate: function() {
    auth();
  },

  LoadData: function(key) {
    loadUserData(UTF8ToString(key));
  },
  
  SaveData : function(key, data){
    saveUserData(UTF8ToString(key),UTF8ToString(data));
  },

  ShowInterstitialAd: function (placementId) {
    showInterstitialAd(UTF8ToString(placementId));
  },

  ShowRewardedAd: function(placementId) {
    showRewardedAd(UTF8ToString(placementId));
  },

  GetDeviceInfo: function() {
    getDeviceInfo();
  },

  RequestReview: function() {
    requestReview();
  },

  RequestLeaderboardEntries: function(name) {
    getLeaderboardEntries(UTF8ToString(name));
  },

  SetLeaderboardScore: function(id, score) {
     setLeaderboardScore(UTF8ToString(id),score);
  },

  ConsoleLog: function(message) {
    consolelog(UTF8ToString(message));
  },

  Purchase: function(productId) {
    purchase(UTF8ToString(productId));
  },

  InitPurchase: function() {
    initPurchases();
  },

  GetPurchases: function() {
    getPurchases();
  }

});