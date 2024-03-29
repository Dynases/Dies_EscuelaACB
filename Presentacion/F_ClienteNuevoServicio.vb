﻿
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports Presentacion.F1_ServicioVenta

Public Class F_ClienteNuevoServicio
    Dim TableVehiculo As DataTable
    Public placaV As String = ""
    Public Cliente As Boolean = False


    Public Sub _priniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prCargarComboLibreria(cbmarca, gi_LibVEHICULO, gi_LibVEHIMarca)
        _prCargarComboLibreria(cbmodelo, gi_LibVEHICULO, gi_LibVEHIModelo)
        _prCargarComboLibreria(cbtipo, gi_LibVEHICULO, 4)
        _prCargarComboLibreriaTipoCliente(cbTipoCliente, 14, 4)

        cbTipoCliente.Value = 1
        tbNombre.CharacterCasing = CharacterCasing.Upper
        tbplaca.CharacterCasing = CharacterCasing.Upper
        cbmarca.CharacterCasing = CharacterCasing.Upper
        cbmodelo.CharacterCasing = CharacterCasing.Upper

        TableVehiculo = L_prClienteLVehiculo(-1)
        _LengthTextBox()
        If (Not placaV.Equals("")) Then
            tbplaca.Text = placaV

        End If
    End Sub
    Public Sub _LengthTextBox()
        tbNombre.MaxLength = 30
        tbroseta.MaxLength = 10
        tbplaca.MaxLength = 10
    End Sub

    Public Sub _prHabilitarFocus()
        With MHighlighterFocus
            .SetHighlightOnFocus(tbNombre, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(cbmarca, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(cbmodelo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbplaca, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbroseta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(cbtipo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnguardar, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnsalir, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        End With
    End Sub
    Public Function _prValidar() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbNombre.Text = String.Empty Then
            tbNombre.BackColor = Color.Red
            MEP.SetError(tbNombre, "Ingrese nombre del cliente!".ToUpper)
            _ok = False
        Else
            tbNombre.BackColor = Color.White
            MEP.SetError(tbNombre, "")
        End If
        If tbplaca.Text = String.Empty Then
            tbplaca.BackColor = Color.Red
            MEP.SetError(tbplaca, "Ingrese placa del vehiculo!".ToUpper)
            _ok = False
        Else
            tbplaca.BackColor = Color.White
            MEP.SetError(tbplaca, "")
        End If
        If cbmarca.SelectedIndex < 0 Then
            cbmarca.BackColor = Color.Red
            MEP.SetError(cbmarca, "Seleccione marca del vehiculo!".ToUpper)
            _ok = False
        Else
            cbmarca.BackColor = Color.White
            MEP.SetError(cbmarca, "")
        End If
        If cbTipoCliente.SelectedIndex < 0 Then
            cbTipoCliente.BackColor = Color.Red
            MEP.SetError(cbTipoCliente, "Seleccione Tipo de Cliente!".ToUpper)
            _ok = False
        Else
            cbTipoCliente.BackColor = Color.White
            MEP.SetError(cbTipoCliente, "")
        End If

        If cbmodelo.SelectedIndex < 0 Then
            cbmodelo.BackColor = Color.Red
            MEP.SetError(cbmodelo, "Seleccione modelo del vehiculo!".ToUpper)
            _ok = False
        Else
            cbmodelo.BackColor = Color.White
            MEP.SetError(cbmodelo, "")
        End If
        If cbtipo.SelectedIndex < 0 Then
            cbtipo.BackColor = Color.Red
            MEP.SetError(cbtipo, "Seleccione modelo del vehiculo!".ToUpper)
            _ok = False
        Else
            cbtipo.BackColor = Color.White
            MEP.SetError(cbtipo, "")
        End If


        If (tbplaca.Text.Length < 6) Then
            tbplaca.BackColor = Color.Red
            MEP.SetError(tbplaca, "Ingrese Formato de Placa Correcto".ToUpper)
            _ok = False
        Else
            tbplaca.BackColor = Color.White
            MEP.SetError(tbplaca, "")
        End If

        Dim cadenas As String = tbplaca.Text
        Dim cont As Integer = cadenas.Length - 1
        If (tbplaca.Text.Length >= 6) Then
            Dim char07 = cadenas(cont)
            Dim char06 = cadenas(cont - 1)
            Dim char05 = cadenas(cont - 2)
            Dim char04 = cadenas(cont - 3)

            If (char07 Like "*[a-zA-Z]*" And char06 Like "*[a-zA-Z]*" And char05 Like "*[a-zA-Z]*" And IsNumeric(char04)) Then
                tbplaca.BackColor = Color.White
                MEP.SetError(tbplaca, "")

            Else
                tbplaca.BackColor = Color.Red
                MEP.SetError(tbplaca, "Ingrese Formato de Placa Correcto".ToUpper)
                _ok = False
            End If

        End If
        MHighlighterFocus.UpdateHighlights()
        Return _ok

    End Function
    Private Sub F_ClienteNuevoServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _priniciarTodo()
        _prHabilitarFocus()

    End Sub


    Private Sub ReflectionLabel1_Click(sender As Object, e As EventArgs) Handles ReflectionLabel1.Click

    End Sub

    Public Sub _prCargarDatosVehiculo()
        TableVehiculo.Rows().Add()
        TableVehiculo.Rows(0).Item("lbnumi") = 0
        TableVehiculo.Rows(0).Item("lbmar") = cbmarca.Value
        TableVehiculo.Rows(0).Item("lbmod") = cbmodelo.Value
        TableVehiculo.Rows(0).Item("lbtip1_4") = cbtipo.Value
        TableVehiculo.Rows(0).Item("lbplac") = tbplaca.Text
        placaV = tbplaca.Text
        If (tbroseta.Text = String.Empty) Then
            TableVehiculo.Rows(0).Item("lbros") = 0
        Else
            If (IsNumeric(tbroseta.Text)) Then
                TableVehiculo.Rows(0).Item("lbros") = tbroseta.Text
            End If

        End If
        TableVehiculo.Rows(0).Item("estado") = 0
        TableVehiculo.Rows(0).Item("lbimg") = "Imagen"
        TableVehiculo.Rows(0).Item("lblin") = 100
        TableVehiculo.Rows(0).Item("desmarc") = cbmarca.Text
        TableVehiculo.Rows(0).Item("descmod") = cbmodelo.Text

    End Sub
    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        If (_prValidar()) Then
            _prCargarDatosVehiculo()
            Dim res As Boolean = L_prClienteLVentaGrabar("", cbTipoCliente.Value, "0", Now.Date.ToString, "", tbNombre.Text, "", "", "", "", "", "", "", 1, "", "", TableVehiculo)
            If res Then

              

                ToastNotification.Show(Me, "EL Cliente : ".ToUpper + tbNombre.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

                Cliente = True
                Me.Close()
                


            End If


        End If
    End Sub

    Private Sub tbNombre_KeyDown(sender As Object, e As KeyEventArgs) Handles tbNombre.KeyDown
        If (e.KeyData = Keys.Enter) Then
            cbmarca.Focus()

        End If
    End Sub

    Private Sub cbmarca_KeyDown(sender As Object, e As KeyEventArgs) Handles cbmarca.KeyDown
        If (e.KeyData = Keys.Enter) Then
            If (BtnMarca.Visible = True) Then
                BtnMarca.Focus()
            Else
                cbmodelo.Focus()
            End If


        End If
    End Sub

    Private Sub cbmodelo_KeyDown(sender As Object, e As KeyEventArgs) Handles cbmodelo.KeyDown
        If (e.KeyData = Keys.Enter) Then
            If (BtnModelo.Visible = True) Then
                BtnModelo.Focus()
            Else
                cbtipo.Focus()
            End If


        End If
    End Sub

    Private Sub cbtipo_KeyDown(sender As Object, e As KeyEventArgs) Handles cbtipo.KeyDown
        If (e.KeyData = Keys.Enter) Then
            cbTipoCliente.Focus()

        End If
    End Sub

    Private Sub tbplaca_KeyDown(sender As Object, e As KeyEventArgs) Handles tbplaca.KeyDown
        If (e.KeyData = Keys.Enter) Then
            tbroseta.Focus()

        End If
    End Sub

    Private Sub tbroseta_KeyDown(sender As Object, e As KeyEventArgs) Handles tbroseta.KeyDown
        If (e.KeyData = Keys.Enter) Then
            btnguardar.Focus()

        End If
    End Sub

    Private Sub tbroseta_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbroseta.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsSymbol(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsSeparator(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

        tbroseta.Text = Trim(Replace(tbroseta.Text, "  ", " "))
        tbroseta.Select(tbroseta.Text.Length, 0)
    End Sub

    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Me.Close()

    End Sub

    Private Sub cbmarca_ValueChanged(sender As Object, e As EventArgs) Handles cbmarca.ValueChanged
        If cbmarca.SelectedIndex < 0 And cbmarca.Text <> String.Empty Then
            BtnMarca.Visible = True
        Else
            BtnMarca.Visible = False
        End If
    End Sub

    Private Sub cbmodelo_ValueChanged(sender As Object, e As EventArgs) Handles cbmodelo.ValueChanged
        If cbmodelo.SelectedIndex < 0 And cbmodelo.Text <> String.Empty Then
            BtnModelo.Visible = True
        Else
            BtnModelo.Visible = False
        End If
    End Sub

    Private Sub BtnMarca_Click(sender As Object, e As EventArgs) Handles BtnMarca.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, gi_LibVEHICULO, gi_LibVEHIMarca, cbmarca.Text, "") Then
            _prCargarComboLibreria(cbmarca, gi_LibVEHICULO, gi_LibVEHIMarca)
            cbmarca.SelectedIndex = CType(cbmarca.DataSource, DataTable).Rows.Count - 1
        End If
        cbmodelo.Focus()

    End Sub

    Private Sub BtnModelo_Click(sender As Object, e As EventArgs) Handles BtnModelo.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, gi_LibVEHICULO, gi_LibVEHIModelo, cbmodelo.Text, "") Then
            _prCargarComboLibreria(cbmodelo, gi_LibVEHICULO, gi_LibVEHIModelo)
            cbmodelo.SelectedIndex = CType(cbmodelo.DataSource, DataTable).Rows.Count - 1
        End If
        cbtipo.Focus()

    End Sub

    Private Sub LabelX20_Click(sender As Object, e As EventArgs) Handles LabelX20.Click

    End Sub

    Private Sub cbTipoCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles cbTipoCliente.KeyDown
        If (e.KeyData = Keys.Enter) Then
            tbplaca.Focus()

        End If
    End Sub
End Class