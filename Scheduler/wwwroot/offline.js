window.localStorageHelper = {
    setItem: function(key, value) {
        localStorage.setItem(key, value);
    },
    getItem: function(key) {
        return localStorage.getItem(key);
    },
    removeItem: function(key) {
        localStorage.removeItem(key);
    },
    isOnline: function() {
        return navigator.onLine;
    },
    savePendingSubmission: function(data) {
        let submissions = JSON.parse(localStorage.getItem('pendingSubmissions')) || [];
        submissions.push(data);
        localStorage.setItem('pendingSubmissions', JSON.stringify(submissions));
    },
    getPendingSubmissions: function() {
        return JSON.parse(localStorage.getItem('pendingSubmissions')) || [];
    },
    clearPendingSubmissions: function() {
        localStorage.removeItem('pendingSubmissions');
    }
};

window.addEventListener('offline', () => {
    DotNet.invokeMethodAsync('Scheduler', 'SetOnlineStatus', false);
});

window.addEventListener('online', () => {
    DotNet.invokeMethodAsync('Scheduler', 'SetOnlineStatus', true);
});
