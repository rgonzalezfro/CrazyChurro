<?xml version="1.0" encoding="UTF-8"?>
<tileset version="1.8" tiledversion="1.8.2" name="churrero" tilewidth="16" tileheight="16" tilecount="512" columns="32">
 <image source="main_tileset.png" width="512" height="256"/>
 <tile id="12">
  <animation>
   <frame tileid="12" duration="200"/>
   <frame tileid="76" duration="200"/>
   <frame tileid="44" duration="200"/>
   <frame tileid="44" duration="200"/>
   <frame tileid="76" duration="200"/>
   <frame tileid="44" duration="200"/>
   <frame tileid="76" duration="200"/>
   <frame tileid="76" duration="200"/>
   <frame tileid="44" duration="200"/>
   <frame tileid="12" duration="200"/>
   <frame tileid="12" duration="200"/>
   <frame tileid="44" duration="200"/>
  </animation>
 </tile>
 <wangsets>
  <wangset name="parque" type="corner" tile="-1">
   <wangcolor name="pasto" color="#ff0000" tile="-1" probability="1"/>
   <wangcolor name="tierra" color="#00ff00" tile="-1" probability="1"/>
   <wangcolor name="cemento" color="#0000ff" tile="-1" probability="1"/>
   <wangcolor name="agua" color="#aa007f" tile="-1" probability="1"/>
   <wangtile tileid="3" wangid="0,0,0,1,0,0,0,0"/>
   <wangtile tileid="4" wangid="0,0,0,1,0,1,0,0"/>
   <wangtile tileid="5" wangid="0,0,0,0,0,1,0,0"/>
   <wangtile tileid="6" wangid="0,0,0,3,0,0,0,0"/>
   <wangtile tileid="7" wangid="0,0,0,3,0,3,0,0"/>
   <wangtile tileid="8" wangid="0,0,0,0,0,3,0,0"/>
   <wangtile tileid="9" wangid="0,0,0,2,0,0,0,0"/>
   <wangtile tileid="10" wangid="0,0,0,2,0,2,0,0"/>
   <wangtile tileid="11" wangid="0,0,0,0,0,2,0,0"/>
   <wangtile tileid="12" wangid="0,4,0,4,0,4,0,4"/>
   <wangtile tileid="13" wangid="0,1,0,1,0,1,0,1"/>
   <wangtile tileid="14" wangid="0,1,0,1,0,1,0,1"/>
   <wangtile tileid="15" wangid="0,1,0,1,0,1,0,0"/>
   <wangtile tileid="16" wangid="0,0,0,1,0,1,0,1"/>
   <wangtile tileid="17" wangid="0,2,0,2,0,2,0,0"/>
   <wangtile tileid="18" wangid="0,0,0,2,0,2,0,2"/>
   <wangtile tileid="19" wangid="0,3,0,3,0,3,0,0"/>
   <wangtile tileid="20" wangid="0,0,0,3,0,3,0,3"/>
   <wangtile tileid="35" wangid="0,1,0,1,0,0,0,0"/>
   <wangtile tileid="36" wangid="0,1,0,1,0,1,0,1"/>
   <wangtile tileid="37" wangid="0,0,0,0,0,1,0,1"/>
   <wangtile tileid="38" wangid="0,3,0,3,0,0,0,0"/>
   <wangtile tileid="39" wangid="0,3,0,3,0,3,0,3"/>
   <wangtile tileid="40" wangid="0,0,0,0,0,3,0,3"/>
   <wangtile tileid="41" wangid="0,2,0,2,0,0,0,0"/>
   <wangtile tileid="42" wangid="0,2,0,2,0,2,0,2"/>
   <wangtile tileid="43" wangid="0,0,0,0,0,2,0,2"/>
   <wangtile tileid="44" wangid="0,4,0,4,0,4,0,4"/>
   <wangtile tileid="45" wangid="0,1,0,1,0,1,0,1"/>
   <wangtile tileid="47" wangid="0,1,0,1,0,0,0,1"/>
   <wangtile tileid="48" wangid="0,1,0,0,0,1,0,1"/>
   <wangtile tileid="49" wangid="0,2,0,2,0,0,0,2"/>
   <wangtile tileid="50" wangid="0,2,0,0,0,2,0,2"/>
   <wangtile tileid="51" wangid="0,3,0,3,0,0,0,3"/>
   <wangtile tileid="52" wangid="0,3,0,0,0,3,0,3"/>
   <wangtile tileid="67" wangid="0,1,0,0,0,0,0,0"/>
   <wangtile tileid="68" wangid="0,1,0,0,0,0,0,1"/>
   <wangtile tileid="69" wangid="0,0,0,0,0,0,0,1"/>
   <wangtile tileid="70" wangid="0,3,0,0,0,0,0,0"/>
   <wangtile tileid="71" wangid="0,3,0,0,0,0,0,3"/>
   <wangtile tileid="72" wangid="0,0,0,0,0,0,0,3"/>
   <wangtile tileid="73" wangid="0,2,0,0,0,0,0,0"/>
   <wangtile tileid="74" wangid="0,2,0,0,0,0,0,2"/>
   <wangtile tileid="75" wangid="0,0,0,0,0,0,0,2"/>
   <wangtile tileid="76" wangid="0,4,0,4,0,4,0,4"/>
   <wangtile tileid="77" wangid="0,1,0,1,0,1,0,1"/>
  </wangset>
  <wangset name="caminos" type="edge" tile="209">
   <wangcolor name="vereda" color="#00ff00" tile="-1" probability="1"/>
   <wangcolor name="ladrillo_pared" color="#ffaa00" tile="-1" probability="1"/>
   <wangtile tileid="109" wangid="0,0,2,0,2,0,0,0"/>
   <wangtile tileid="110" wangid="0,0,2,0,0,0,2,0"/>
   <wangtile tileid="111" wangid="0,0,0,0,2,0,2,0"/>
   <wangtile tileid="141" wangid="2,0,0,0,2,0,0,0"/>
   <wangtile tileid="143" wangid="2,0,0,0,2,0,0,0"/>
   <wangtile tileid="146" wangid="1,0,0,0,1,0,0,0"/>
   <wangtile tileid="173" wangid="2,0,2,0,0,0,0,0"/>
   <wangtile tileid="174" wangid="0,0,2,0,0,0,2,0"/>
   <wangtile tileid="175" wangid="2,0,0,0,0,0,2,0"/>
   <wangtile tileid="177" wangid="0,0,1,0,0,0,1,0"/>
   <wangtile tileid="178" wangid="1,0,1,0,1,0,1,0"/>
   <wangtile tileid="179" wangid="0,0,1,0,0,0,1,0"/>
   <wangtile tileid="210" wangid="1,0,0,0,1,0,0,0"/>
   <wangtile tileid="241" wangid="0,0,1,0,1,0,0,0"/>
   <wangtile tileid="242" wangid="0,0,1,0,0,0,1,0"/>
   <wangtile tileid="243" wangid="0,0,0,0,1,0,1,0"/>
   <wangtile tileid="244" wangid="0,0,0,0,1,0,0,0"/>
   <wangtile tileid="245" wangid="0,0,1,0,0,0,0,0"/>
   <wangtile tileid="246" wangid="0,0,0,0,0,0,1,0"/>
   <wangtile tileid="273" wangid="1,0,0,0,1,0,0,0"/>
   <wangtile tileid="275" wangid="1,0,0,0,1,0,0,0"/>
   <wangtile tileid="276" wangid="1,0,0,0,0,0,0,0"/>
   <wangtile tileid="305" wangid="1,0,1,0,0,0,0,0"/>
   <wangtile tileid="306" wangid="0,0,1,0,0,0,1,0"/>
   <wangtile tileid="307" wangid="1,0,0,0,0,0,1,0"/>
  </wangset>
 </wangsets>
</tileset>
