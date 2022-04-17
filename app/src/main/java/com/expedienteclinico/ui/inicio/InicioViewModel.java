package com.expedienteclinico.ui.inicio;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.expedienteclinico.Controlador.SessionUsuario;

public class InicioViewModel extends ViewModel {

    private final MutableLiveData<String> mText;

    String correo = "";
    private SessionUsuario sessionUsuario = SessionUsuario.getInstance();

    public InicioViewModel() {
        mText = new MutableLiveData<>();
        mText.setValue("INICIO");
        //mText.setValue(sessionUsuario.getCorreo()+"\n"+sessionUsuario.getIdUsuario());

    }

    public LiveData<String> getText() {
        return mText;
    }
}