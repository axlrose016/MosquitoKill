FBInstant.initializeAsync()
.then(function() {
	FBInstant.setLoadingProgress(100);
});

FBInstant.startGameAsync()
.then(function(){
	game.start();
})