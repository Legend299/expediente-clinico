package com.expedienteclinico.utilidades;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class ConvertirFecha {

    public Date recibirFecha(String fecha){

        try {
            SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
            Date date = sdf.parse(fecha);
            return date;
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return null;
    }

    public Date recibirFechaDatePicker(String fecha){
        try {
            DateFormat format = new SimpleDateFormat("dd/MMMM/yyyy", Locale.getDefault());
            Date date = format.parse(fecha);
            return date;
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return null;
    }

    public String DateToString(Date date, String formato){

        SimpleDateFormat sdf = new SimpleDateFormat();
        sdf.applyPattern(formato);
        String fecha = sdf.format(date);

        return fecha;
    }

}
