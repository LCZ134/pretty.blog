import React from 'react';

function Map() {

  React.useEffect(() => {
    var map = new BMap.Map("map");    // 创建Map实例
    //添加地图类型控件
    map.addControl(new BMap.MapTypeControl({
      mapTypes: [
        BMAP_NORMAL_MAP,
        BMAP_HYBRID_MAP
      ]
    }));
    var address = '东莞市长安镇';
    map.setCurrentCity(address);          // 设置地图显示的城市 此项是必须设置的
    // 创建地址解析器实例
    var myGeo = new BMap.Geocoder();
    // 将地址解析结果显示在地图上,并调整地图视野
    myGeo.getPoint(address, function (point) {
      if (point) {
        map.centerAndZoom(point, 9);
        map.addOverlay(new BMap.Marker(point));
        map.enableScrollWheelZoom(false);     //开启鼠标滚轮缩放
      } else {
        alert("您选择地址没有解析到结果!");
      }
    }, address);
  });
  return (
    <section className="map card">

      <div id="map" he></div>
    </section>
  )
}

export default Map;