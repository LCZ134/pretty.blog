const commonOptions = {
  chunks: 'all',
  reuseExistingChunk: true
}

module.exports = {
  namedChunks: true,
  runtimeChunk: {
    name: 'manifest'
  },
  splitChunks: {
    maxInitialRequests: 5,
    cacheGroups: {
      polyfill: {
        test: /[\\/]node_modules[\\/](core-js|raf|@babel|babel)[\\/]/,
        name: 'polyfill',
        priority: 2,
        ...commonOptions
      },
      dll: {
        test: /[\\/]node_modules[\\/](react|react-dom)[\\/]/,
        name: 'dll',
        priority: 1,
        ...commonOptions
      },
      commons: {
        name: 'commons',
        minChunks: 2, // 至少被1/3页面的引入才打入common包
        ...commonOptions
      }
    }
  }
}