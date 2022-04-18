package com.expedienteclinico;

public enum API {
    HOST_PRIVADO("192.68.1.69"),
    HOST_PUBLICO("legend.zapto.org"),
    QUERY_EXPEDIENTE_ID("http://192.168.1.69:8891/api/Expediente/"),
    QUERY_USUARIO_LISTA("http://192.168.1.69:8891/api/usuario/"),
    QUERY_ESTADOS_LISTA("http://192.168.1.69:8891/api/usuario/estados"),
    QUERY_USUARIO_ID(""),
    QUERY_CONSULTA_ID_EXPEDIENTE("http://192.168.1.69:8891/api/Consulta/");

    private final String str;

    API(final String str){
        this.str = str;
    }

    @Override
    public String toString(){
        return str;
    }

}
