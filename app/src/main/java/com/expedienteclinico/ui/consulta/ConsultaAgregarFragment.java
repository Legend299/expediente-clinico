package com.expedienteclinico.ui.consulta;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView;
import android.widget.ProgressBar;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;

import com.expedienteclinico.Controlador.Consulta.ServicioConsulta;
import com.expedienteclinico.Controlador.RolesUsuario;
import com.expedienteclinico.Controlador.SessionUsuario;
import com.expedienteclinico.R;
import com.expedienteclinico.databinding.FragmentConsultaAgregarBinding;
import com.expedienteclinico.databinding.FragmentConsultaBinding;
import com.expedienteclinico.dto.ConsultaDTO;

import java.util.List;

public class ConsultaAgregarFragment extends Fragment {

    private FragmentConsultaAgregarBinding binding;
    private ProgressBar carga;
    private WebView web;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        ConsultaViewModel consultaViewModel =
                new ViewModelProvider(this).get(ConsultaViewModel.class);

        binding = FragmentConsultaAgregarBinding.inflate(inflater, container, false);
        View root = binding.getRoot();

        carga = root.findViewById(R.id.idLoadingPB);

        //carga.setVisibility(View.VISIBLE);

        //final ListView textView = binding.listViewConsulta;
        //consultaViewModel.getText().observe(getViewLifecycleOwner(), textView::setText);
        return root;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }

}
