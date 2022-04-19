package com.expedienteclinico.ui.expediente;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.expedienteclinico.Controlador.SessionUsuario;

public class ExpedienteViewModel extends ViewModel {

    private final MutableLiveData<String> mText;

    String correo = "";
    private SessionUsuario sessionUsuario = SessionUsuario.getInstance();

    public ExpedienteViewModel() {
        mText = new MutableLiveData<>();
        mText.setValue("This is expediente fragment");
        //mText.setValue(sessionUsuario.getCorreo()+"\n"+sessionUsuario.getIdUsuario());

    }

    public LiveData<String> getText() {
        return mText;
    }
}
