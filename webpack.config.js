var ExtractTextPlugin = require ("extract-text-webpack-plugin");
var path = require ("path");

var jsxConfig = {
    mode: "production",
    entry: {
        MiniGallery: "./R7.MiniGallery/Views/Image/MiniGallery.jsx"
    },
    output: {
        path: path.resolve (__dirname, "R7.MiniGallery/assets/js"),
        filename: "[name].min.js"
    },
    module: {
        rules: [
            {
                test: /\.jsx$/,
                exclude: /(node_modules|bower_components)/,
                use: {
                    loader: "babel-loader",
                    options: {
                        presets: ["@babel/preset-react", "@babel/preset-env"]
                    }
                }
            }
        ]
    },
};

var jsConfig = {
    mode: "production",
    entry: {
        quickSettings: "./R7.MiniGallery/assets/js/quickSettings.js"
    },
    output: {
        path: path.resolve (__dirname, "R7.MiniGallery/assets/js"),
        filename: "[name].min.js"
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /(node_modules|bower_components)/,
                use: {
                    loader: "babel-loader",
                    options: {
                        presets: ["@babel/preset-env"]
                    }
                }
            }
        ]
    },
};

module.exports = [jsxConfig, jsConfig];
