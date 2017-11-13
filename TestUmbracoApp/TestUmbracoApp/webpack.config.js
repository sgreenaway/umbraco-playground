const path = require('path');

module.exports = {
    entry: './app/manifest.js',
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'dist')
    }
};