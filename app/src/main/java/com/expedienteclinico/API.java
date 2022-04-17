package com.expedienteclinico;

public enum API {
    QUERY_EXPEDIENTE_ID("http://192.168.1.69:8891/api/Expediente/"),
    QUERY_USUARIO_LISTA("http://192.168.1.69:8891/api/usuario/"),
    QUERY_USUARIO_ID(""),
    QUERY_CONSULTA("");

    private final String str;

    API(final String str){
        this.str = str;
    }

    @Override
    public String toString(){
        return str;
    }

}
