<template>
  <v-container>
    <v-form ref="form" v-model="valid" @submit.prevent="submitForm">
      <v-col>
       
          <v-toolbar-title>User Registration</v-toolbar-title>
       
      </v-col>
      <v-row>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="user.FName"
            :rules="[rules.required]"
            label="First Name"
            required
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="user.LName"
            :rules="[rules.required]"
            label="Last Name"
            required
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="user.Email"
            :rules="[rules.required, rules.email]"
            label="Email"
            required
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="user.PhoneNumber"
            :rules="[rules.required, rules.phone]"
            label="Phone Number"
            required
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12">
          <v-textarea
            v-model="user.Address"
            :rules="[rules.required]"
            label="Address"
            required
          ></v-textarea>
        </v-col>
      </v-row>
      <v-btn color="primary" type="submit">Submit</v-btn>&nbsp;
      <v-btn color="secondary" @click="clearForm">Clear</v-btn>
    </v-form>
  </v-container>
</template>

<script setup>
import { ref } from 'vue';
import axios from 'axios';

const valid = ref(false);
const user = ref({
  FName: '',
  LName: '',
  Email: '',
  Address: '',
  PhoneNumber: '',
});

const rules = {
  required: value => !!value || 'Required.',
  email: value => /.+@.+\..+/.test(value) || 'E-mail must be valid.',
  phone: value => /^\d+$/.test(value) || 'Phone number must be valid.',
};

const submitForm = async () => {
  if (valid.value) {
    try {
      const response = await axios.post('http://localhost:5099/api/User/SaveUser', user.value);

      if (response.status === 200) {
        console.log('User created successfully');
        clearForm();
        // Optionally, clear the form or navigate to another page
      } else {
        console.error('Failed to create user');
      }
    } catch (error) {
      console.error('Error:', error);
    }
  }
};
const clearForm = () => {
  user.value = {
    fName: '',
    lName: '',
    email: '',
    address: '',
    phoneNumber: '',
  };
};
</script>

<style scoped>
/* Add any scoped styles here if needed */
</style>
