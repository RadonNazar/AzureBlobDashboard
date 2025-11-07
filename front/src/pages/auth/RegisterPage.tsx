import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import Divider from "@mui/material/Divider";
import FormLabel from "@mui/material/FormLabel";
import FormControl from "@mui/material/FormControl";
import { Link, useNavigate } from "react-router";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import Stack from "@mui/material/Stack";
import MuiCard from "@mui/material/Card";
import { styled } from "@mui/material/styles";
import {
    GoogleIcon,
    FacebookIcon,
    SitemarkIcon,
} from "./components/CustomIcons";
import { useFormik } from "formik";
import type { RegisterModel } from "./types";
import axios from "axios";
import { apiBaseUrl } from "../../env";
import type { ServiceResponse } from "../../services/types";

const Card = styled(MuiCard)(({ theme }) => ({
    display: "flex",
    flexDirection: "column",
    alignSelf: "center",
    width: "100%",
    padding: theme.spacing(4),
    gap: theme.spacing(2),
    margin: "auto",
    [theme.breakpoints.up("sm")]: {
        maxWidth: "450px",
    },
    boxShadow:
        "hsla(220, 30%, 5%, 0.05) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.05) 0px 15px 35px -5px",
    ...theme.applyStyles("dark", {
        boxShadow:
            "hsla(220, 30%, 5%, 0.5) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.08) 0px 15px 35px -5px",
    }),
}));

const SignInContainer = styled(Stack)(({ theme }) => ({
    minHeight: "100%",
    padding: theme.spacing(2),
    [theme.breakpoints.up("sm")]: {
        padding: theme.spacing(4),
    },
    "&::before": {
        content: '""',
        display: "block",
        position: "absolute",
        zIndex: -1,
        inset: 0,
        backgroundImage:
            "radial-gradient(ellipse at 50% 50%, hsl(210, 100%, 97%), hsl(0, 0%, 100%))",
        backgroundRepeat: "no-repeat",
        ...theme.applyStyles("dark", {
            backgroundImage:
                "radial-gradient(at 50% 50%, hsla(210, 100%, 16%, 0.5), hsl(220, 30%, 5%))",
        }),
    },
}));

const RegisterPage = () => {
    const initValues: RegisterModel = {
        email: "",
        userName: "",
        password: "",
        confirmPassword: "",
        firstName: "",
        lastName: ""
    };

    const navigate = useNavigate();

    const handleSubmit = async (values: RegisterModel) => {
        try {
            const response = await axios<ServiceResponse<null>>({
                url: `${apiBaseUrl}/auth/register`,
                method: "post",
                data: values,
            });

            const { data } = response;
            console.log(data.message);
            navigate("/login");
        } catch (error: any) {
            if ("response" in error) {
                const { response } = error;
                const { data } = response;
                console.log(data);
            }
        }
    };

    // formik
    const formik = useFormik<RegisterModel>({
        initialValues: initValues,
        onSubmit: handleSubmit,
    });

    return (
        <>
            <CssBaseline enableColorScheme />
            <SignInContainer direction="column" justifyContent="space-between">
                <Card variant="outlined">
                    <SitemarkIcon />
                    <Typography
                        component="h1"
                        variant="h4"
                        sx={{
                            width: "100%",
                            fontSize: "clamp(2rem, 10vw, 2.15rem)",
                        }}
                    >
                        Sign up
                    </Typography>
                    <Box
                        component="form"
                        onSubmit={formik.handleSubmit}
                        noValidate
                        sx={{
                            display: "flex",
                            flexDirection: "column",
                            width: "100%",
                            gap: 2,
                        }}
                    >
                        <FormControl>
                            <FormLabel htmlFor="email">Email</FormLabel>
                            <TextField
                                id="email"
                                type="email"
                                name="email"
                                placeholder="email@mail.com"
                                autoComplete="email"
                                autoFocus
                                required
                                fullWidth
                                variant="outlined"
                                color="primary"
                                onChange={formik.handleChange}
                                value={formik.values.email}
                                onBlur={formik.handleBlur}
                            />
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="userName">User Name</FormLabel>
                            <TextField
                                id="userName"
                                type="text"
                                name="userName"
                                placeholder="username"
                                autoComplete="username"
                                required
                                fullWidth
                                variant="outlined"
                                color="primary"
                                onChange={formik.handleChange}
                                value={formik.values.userName}
                                onBlur={formik.handleBlur}
                            />
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="password">Password</FormLabel>
                            <TextField
                                name="password"
                                placeholder="••••••"
                                type="password"
                                id="password"
                                autoComplete="current-password"
                                required
                                fullWidth
                                variant="outlined"
                                color="primary"
                                onChange={formik.handleChange}
                                value={formik.values.password}
                                onBlur={formik.handleBlur}
                            />
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="confirmPassword">Confirm password</FormLabel>
                            <TextField
                                name="confirmPassword"
                                placeholder="••••••"
                                type="password"
                                id="confirmPassword"
                                autoComplete="current-password"
                                required
                                fullWidth
                                variant="outlined"
                                color="primary"
                                onChange={formik.handleChange}
                                value={formik.values.confirmPassword}
                                onBlur={formik.handleBlur}
                            />
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="firstName">First name</FormLabel>
                            <TextField
                                name="firstName"
                                placeholder="••••••"
                                type="text"
                                id="firstName"
                                fullWidth
                                variant="outlined"
                                color="primary"
                                onChange={formik.handleChange}
                                value={formik.values.firstName}
                                onBlur={formik.handleBlur}
                            />
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="lastName">Last name</FormLabel>
                            <TextField
                                name="lastName"
                                placeholder="••••••"
                                type="text"
                                id="lastName"
                                fullWidth
                                variant="outlined"
                                color="primary"
                                onChange={formik.handleChange}
                                value={formik.values.lastName}
                                onBlur={formik.handleBlur}
                            />
                        </FormControl>
                        <Button type="submit" fullWidth variant="contained">
                            Sign up
                        </Button>
                    </Box>
                    <Divider>or</Divider>
                    <Box
                        sx={{
                            display: "flex",
                            flexDirection: "column",
                            gap: 2,
                        }}
                    >
                        <Button
                            fullWidth
                            variant="outlined"
                            onClick={() => alert("Sign in with Google")}
                            startIcon={<GoogleIcon />}
                        >
                            Sign in with Google
                        </Button>
                        <Button
                            fullWidth
                            variant="outlined"
                            onClick={() => alert("Sign in with Facebook")}
                            startIcon={<FacebookIcon />}
                        >
                            Sign in with Facebook
                        </Button>
                        <Typography sx={{ textAlign: "center" }}>
                            Have an account?{" "}
                            <Link to="/login">Sign in</Link>
                        </Typography>
                    </Box>
                </Card>
            </SignInContainer>
        </>
    );
};

export default RegisterPage;
