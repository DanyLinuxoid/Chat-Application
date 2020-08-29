var root = __dirname + "/wwwroot";
var webpack = require('webpack');

module.exports = {
    entry: {
        chat: root + "/out/Chat/Chat.js",
        layout: root + "/out/Layout/Layout.js",
    },
    output: {
        path: root + "/bundle/",
        filename: "[name]bundle.js"
    },
    resolve: {
        extensions: [".ts", ".tsx", ".js"]
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                loader: "ts-loader",
            },
        ],
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery'
        }),
    ],
}