import "./App.css";
import { Route, Routes } from "react-router";
import DefaultLayout from "./components/layout/DefaultLayout";
import MainPage from "./pages/mainPage/MainPage";
import LoginPage from "./pages/auth/LoginPage";
import { useEffect } from "react";
import { loginSucess } from "./store/slices/authSlice";
import { useDispatch } from "react-redux";
import GameListPage from "./pages/gamePage/GameListPage";
import CreateGamePage from "./pages/gamePage/CreateGamePage";
import RegisterPage from "./pages/auth/RegisterPage";
import ConfirmEmailPage from "./pages/auth/ConfirmEmailPage";
import { useAppSelector } from "./hooks/reduxHooks";
import NotFoundPage from "./pages/notFound/NotFoundPage";

function App() {
    const dispatch = useDispatch();
    const { user } = useAppSelector((state) => state.auth);

    useEffect(() => {
        // read jwt token
        const token = localStorage.getItem("token");
        if (token) {
            dispatch(loginSucess(token));
        }
    }, []);

    return (
        <>
            <Routes>
                <Route path="/" element={<DefaultLayout />}>
                    <Route index element={<MainPage />} />
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/register" element={<RegisterPage />} />
                    <Route
                        path="/confirmemail"
                        element={<ConfirmEmailPage />}
                    />
                    <Route path="/game">
                        <Route index element={<GameListPage />} />
                        {user && user.roles.includes("admin") && (
                            <Route path="create" element={<CreateGamePage />} />
                        )}
                    </Route>
                    <Route path="*" element={<NotFoundPage />} />
                </Route>
            </Routes>
        </>
    );
}

export default App;
