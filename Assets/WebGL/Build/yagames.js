var player = null;
var sdk = null;
var lb = null;

YaGames.init().then(ysdk => {
  sdk = ysdk;
});

function auth() {
  initPlayer().then(() => {
    getUserData();
  }).catch(err => {
    sdk.auth.openAuthDialog().then(() => {
      initPlayer().then(() => {
        getUserData();
      });
    });
  });
}

function initPlayer() {
  return sdk.getPlayer().then(_player => {
    player = _player;
  }).catch(err => {});
}

function getUserData() {
  if (initPlayer) {
    var data = {
      "id": player.getUniqueID(),
      "name": player.getName(),
      "avatarUrlSmall": player.getPhoto('small'),
      "avatarUrlMedium": player.getPhoto('medium'),
      "avatarUrlLarge": player.getPhoto('large'),
      "isAuthorized" : player.getMode() != "lite"
    };
    unityI.SendMessage('YandexGames', 'AuthenticateSuccess', JSON.stringify(data));
  }
}

function getLeaderBoard() {
  sdk.getLeaderboards().then(_lb => {
    lb = _lb
    console.log(JSON.stringify(lb));
    unityI.SendMessage('YandexGames', 'LeaderBoardReceived', JSON.stringify(lb));
  });
}

function setLeaderboardScore(id, score) {
  console.log("setLeaderboardScore" +id +" "+score);
  sdk.getLeaderboards().then(lb => {
    console.log(id +" "+score);
    lb.setLeaderboardScore(id, score);
  });
}

function getLeaderboardEntries(name) {
  console.log("getLeaderBoardEntries "+name);
  sdk.getLeaderboards().then(lb => {
    lb.getLeaderboardEntries(name, {
      quantityTop: 10,
      includeUser: true,
      quantityAround: 3
    }).then(res => {
      res.entries.forEach(element => {
        element.player.avatarUrlSmall = element.player.getAvatarSrc("small");
        element.player.avatarUrlMedium = element.player.getAvatarSrc("medium");
        element.player.avatarUrlLarge = element.player.getAvatarSrc("large");
      });
      unityI.SendMessage('YandexGames', 'LeaderBoardPlayerRatingReceived', JSON.stringify(res));
    });
  });
}

function loadUserData(_key) {
   console.log("loadUserData "+_key);
  sdk.getStorage().then(safeStorage => {
    var data = {
       "key": _key,
       "data": safeStorage.getItem(_key) 
    };
    unityI.SendMessage('YandexGames', 'DataGetting', JSON.stringify(data));
  });
}

function saveUserData(_key, _data) {
  console.log("saveUserData "+_key+" "+_data);
  sdk.getStorage().then(safeStorage => {
    safeStorage.setItem(_key, _data);
    unityI.SendMessage('YandexGames', 'DataSavedSuccess');
  });
}

function getDeviceInfo() {
  var data = {
    "type": sdk.deviceInfo.type
  };
  unityI.SendMessage('YandexGames', 'DeviceInfoReceived', JSON.stringify(data));
}

function requestReview() {
  sdk.feedback.canReview().then(({
    value,
    reason
  }) => {
    if (value) {
      sdk.feedback.requestReview().then(({feedbackSent}) => {
        unityI.SendMessage('YandexGames', 'ReviewSent');
      })
    } else {
      unityI.SendMessage('YandexGames', 'ReviewError', reason);
    }
  })
}

function showInterstitialAd(id) {
  console.log("showInterstitialAd" +" "+id);
  sdk.adv.showFullscreenAdv({
    callbacks: {
      onOpen: () => {
        unityI.SendMessage('YandexGames', 'InterstitialShown', id);
      },
      onClose: function(wasShown) {
        unityI.SendMessage('YandexGames', 'InterstitialClosed', id);
      },
      onError: function(error) {
        var data = {
          "id": id,
          "error": error
        };
        unityI.SendMessage('YandexGames', 'InterstitialFailed', JSON.stringify(data));
      }
    }
  })
}

function showRewardedAd(id) {
  console.log("showRewardedAd" +" "+id);
  sdk.adv.showRewardedVideo({
    callbacks: {
      onOpen: () => {
        unityI.SendMessage('YandexGames', 'RewardedOpen', id);
      },
      onRewarded: () => {
        unityI.SendMessage('YandexGames', 'Rewarded', id);
      },
      onClose: () => {
        unityI.SendMessage('YandexGames', 'RewardedClose', id);
      },
      onError: (e) => {
        var data = {
          "id": id,
          "error": error
        };
        unityI.SendMessage('YandexGames', 'RewardedError', JSON.stringify(data));
      }
    }
  })
}

function consolelog(message){
  console.log(message);
} 